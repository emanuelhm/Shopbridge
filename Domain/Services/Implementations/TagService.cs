using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Application.Dtos.Tag;
using ShopBridge.Domain.Models;
using ShopBridge.Domain.Services.Interfaces;
using ShopBridge.Data;
using ShopBridge.Data.Repository;
using ShopBridge.Application;

namespace ShopBridge.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> logger;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public TagService(Shopbridge_Context _context, ILogger<TagService> _logger, IMapper _mapper)
        {
            repository = new Repository(_context);
            logger = _logger;
            mapper = _mapper;
        }

        public async Task<(bool, string?)> Delete(int id)
        {
            if (await ProductExists(id))
            {
                var tag = await repository.Get((Tag t) => t.Tag_Id == id).FirstOrDefaultAsync();

                if (tag != null)
                {
                    await repository.Delete(tag);
                    return (true, null);
                }
            }

            logger.LogWarning("Product doesn't exists");
            return (false, "Product doesn't exists");
        }

        public async Task<IEnumerable<ResultTagDto>> Get(SearchDto searchDto)
        {
            var query = repository.Get<Tag>();

            if (searchDto.HasPagination())
            {
                query = query.Skip(searchDto.Skip()).Take(searchDto.Size ?? 0);
            }

            if (!string.IsNullOrEmpty(searchDto.Text))
            {
                query = query.Where(c => c.Name.Contains(searchDto.Text));
            }

            return mapper.Map<ResultTagDto[]>(await query.ToListAsync());
        }

        public async Task<ResultTagDto?> Get(int id)
        {
            return mapper.Map<ResultTagDto>(await repository.Get((Tag t) => t.Tag_Id == id).FirstOrDefaultAsync());
        }

        public async Task<(bool, Tag, string?)> Post(TagDto productDto)
        {
            var product = mapper.Map<Tag>(productDto);

            if (!await ProductExists(product.Name))
            {
                await repository.Create(product);
                return (true, product, null);
            }

            logger.LogWarning("Product exists");
            return (false, product, "Product exists");
        }

        public async Task<(bool, Tag?, string?)> Put(int id, TagDto productDto)
        {
            var product = mapper.Map<Tag>(productDto);

            if (!await ProductExists(product.Name, id))
            {
                product.Tag_Id = id;

                await repository.Update(product);
                return (true, product, null);
            }

            logger.LogWarning("Product doesn't exists");
            return (false, product, "Product doesn't exists");
        }

        public async Task<bool> ProductExists(int id)
        {
            return await repository.Get((Tag t) => t.Tag_Id == id).AnyAsync();
        }

        public async Task<bool> ProductExists(string name, int? id = null)
        {
            if (id.HasValue)
            {
                return await repository.Get((Tag t) => t.Name == name && t.Tag_Id != id).AnyAsync();
            }

            return await repository.Get((Tag t) => t.Name == name).AnyAsync();
        }
    }
}
