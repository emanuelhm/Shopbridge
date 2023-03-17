using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Application.Dtos.Tag
{
    public class TagDto
    {
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "")]
        public string Name { get; set; } = null!;
    }
}
