using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GuardianCyberStore.Pages
{
    public class UpdateProductModel : PageModel
    {
        private readonly AppDataContext _db;

        [BindProperty]
        public Product Product { get; set; }

        public UpdateProductModel(AppDataContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int id)
        {
            Product = await _db.Products.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./UpdateProduct", new { id = Product.Id });
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Any(e => e.Id == id);
        }
    }

}
