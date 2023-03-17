using ShopBridge.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "")]
        public string Name { get; set; } = null!;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
