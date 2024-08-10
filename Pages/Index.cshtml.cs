using GuardianCyberStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuardianCyberStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDataContext _context;

        public IList<Category> Categories { get; set; }
        public IList<Product> Products { get; set; }

        public IndexModel(AppDataContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Categories = await _context.categories.ToListAsync();
            Products = await _context.Products.ToListAsync();
        }
    }
}
