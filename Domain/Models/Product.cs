using ShopBridge.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "")]
        public string Description { get; set; } = null!;

        [Required]
        public double Price { get; set; }
        
        public string? PictureUrl { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
