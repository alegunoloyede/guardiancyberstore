using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuardianCyberStore.Pages
{
    public class ProductsModel : PageModel
    {
        public readonly AppDataContext _db;
        public List<Product> ProductList { get; set; }
        public Cart Cart { get; set; }
        public ProductsModel(AppDataContext db)
        {
            _db = db;
            Cart = new Cart();
        }
        public void OnGet()
        {
            ProductList = _db.Products.ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUpdateAsync(int id)
        {
            return RedirectToPage("/UpdateProduct", new { id = id });
        }





        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
                var cartItem = cart.Items.FirstOrDefault(i => i.Product.Id == product.Id);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cart.Items.Add(new CartItem { Product = product, Quantity = 1 });
                }
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToPage();
        }


    }
}
