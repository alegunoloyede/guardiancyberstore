using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GuardianCyberStore.Pages
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        private readonly AppDataContext _db;

        public CategoryModel(AppDataContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IList<Category> Categories { get; set; }
        
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Categories = await _db.categories.ToListAsync();

            if (id == null)
            {
                Category = new Category();
            }
            else
            {
                Category = await _db.categories.FirstOrDefaultAsync(m => m.Id == id);

                if (Category == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Category.Id == 0)
            {
                _db.categories.Add(Category);
            }
            else
            {
                _db.Attach(Category).State = EntityState.Modified;
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Category");
        }

        private bool CategoryExists(int id)
        {
            return _db.categories.Any(e => e.Id == id);
        }
    }
}
