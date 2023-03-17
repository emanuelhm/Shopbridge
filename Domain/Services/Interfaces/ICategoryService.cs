using ShopBridge.Application;
using ShopBridge.Application.Dtos.Category;
using ShopBridge.Domain.Models;

namespace ShopBridge.Domain.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ResultCategoryDto>> Get(SearchDto seachDto);

        Task<ResultCategoryDto?> Get(int id);

        Task<(bool, Category?, string?)> Put(int id, CategoryDto product);

        Task<(bool, Category, string?)> Post(CategoryDto productDto);

        Task<(bool, string?)> Delete(int id);
    }
}
