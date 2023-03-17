using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Application.Dtos.Category
{
    public class CategoryDto
    {
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "")]
        public string Name { get; set; } = null!;
    }
}
