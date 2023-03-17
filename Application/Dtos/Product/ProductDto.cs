using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Application.Dtos.Product
{
    public class ProductDto
    {
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = @"Field ""Name"" should be between 1 and 250 characters long")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = @"Field ""Description"" should be between 1 and 250 characters long")]
        public string Description { get; set; } = null!;

        [Required]
        public double Price { get; set; }

        [MinLength(1)]
        public int[] Categories { get; set; } = null!;

        [MinLength(1)]
        public int[] Tags { get; set; } = null!;

        public IFormFile Image { get; set; } = null!;
    }
}
