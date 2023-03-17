using ShopBridge.Application;
using ShopBridge.Application.Dtos.Tag;
using ShopBridge.Domain.Models;

namespace ShopBridge.Domain.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<ResultTagDto>> Get(SearchDto searchDto);

        Task<ResultTagDto?> Get(int id);

        Task<(bool, Tag?, string?)> Put(int id, TagDto product);

        Task<(bool, Tag, string?)> Post(TagDto productDto);

        Task<(bool, string?)> Delete(int id);
    }
}
