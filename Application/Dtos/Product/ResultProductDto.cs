using ShopBridge.Application.Dtos.Tag;
using ShopBridge.Application.Dtos.Category;

namespace ShopBridge.Application.Dtos.Product
{
    public class ResultProductDto 
    {
        public int Product_Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public List<ResultCategoryDto> Categories { get; set; } = null!;

        public List<ResultTagDto> Tags { get; set; } = null!;
    }
}
