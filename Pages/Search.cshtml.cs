using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GuardianCyberStore.Pages
{
    public class SearchModel : PageModel
    {
        private readonly AppDataContext _db;

        public IList<Product> ProductResults { get; set; }
        public IList<Category> CategoryResults { get; set; }

        public SearchModel(AppDataContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string query)
        {
            ProductResults = await _db.Products
                .Where(p => p.Name.Contains(query))
                .ToListAsync();

            CategoryResults = await _db.categories
                .Where(c => c.Name.Contains(query))
                .ToListAsync();
        }
    }
}
