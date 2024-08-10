using System.ComponentModel.DataAnnotations;

namespace GuardianCyberStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Lenght can't go above 25")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
