using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GuardianCyberStore.Pages
{
    [Authorize]
    public class AddProductModel : PageModel
    {
        public readonly AppDataContext _db;
        public AddProductModel(AppDataContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product product { get; set; }
        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Categories = new SelectList(await _db.categories.ToListAsync(), "Id", "Name");

            if (id == null)
            {
                product = new Product();
                return Page();
            }

            product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToPage("./AddProduct");
        }
    }
}
