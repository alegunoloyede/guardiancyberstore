using GuardianCyberStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuardianCyberStore.Pages
{
    public class CartModel : PageModel
    {
        public Cart Cart { get; set; }

        public void OnGet()
        {
            Cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
        }
    }
}
