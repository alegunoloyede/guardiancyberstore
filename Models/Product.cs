using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GuardianCyberStore.Models
{
    public class Product
    {
       
        public int Id { get; set; }
        
        [StringLength(30, ErrorMessage = "Lenght can't go above 30")]
        public string Name { get; set; }

        public string Description {  get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
              
        public string ProductImage { get; set; }
       
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
