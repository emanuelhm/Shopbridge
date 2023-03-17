using ShopBridge.Application;
using ShopBridge.Application.Dtos.Product;
using ShopBridge.Domain.Models;

namespace ShopBridge.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ResultProductDto>> Get(SearchDto searchDto);

        Task<ResultProductDto?> Get(int id);

        Task<(bool, Product?, string?)> Put(int id, ProductDto product);

        Task<(bool, Product?, string?)> Post(ProductDto productDto);

        Task<(bool, string?)> Delete(int id);
    }
}
