using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Application.Dtos.Category;
using ShopBridge.Domain.Models;
using ShopBridge.Domain.Services.Interfaces;
using ShopBridge.Data;
using ShopBridge.Data.Repository;
using ShopBridge.Application;

namespace ShopBridge.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> logger;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public CategoryService(Shopbridge_Context _context, ILogger<CategoryService> _logger, IMapper _mapper)
        {
            repository = new Repository(_context);
            logger = _logger;
            mapper = _mapper;
        }

        public async Task<(bool, string?)> Delete(int id)
        {
            if (await ProductExists(id))
            {
                var Category = await repository.Get((Category c) => c.Category_Id == id).FirstOrDefaultAsync();

                if (Category != null)
                {
                    await repository.Delete(Category);
                    return (true, null);
                }
            }

            logger.LogWarning("Product doesn't exists");
            return (false, "Product doesn't exists");
        }

        public async Task<IEnumerable<ResultCategoryDto>> Get(SearchDto searchDto)
        {
            var query = repository.Get<Category>();

            if (searchDto.HasPagination())
            {
                query = query.Skip(searchDto.Skip()).Take(searchDto.Size ?? 0);
            }

            if (!string.IsNullOrEmpty(searchDto.Text))
            {
                query = query.Where(c => c.Name.Contains(searchDto.Text));
            }

            return mapper.Map<ResultCategoryDto[]>(await query.ToListAsync());
        }

        public async Task<ResultCategoryDto?> Get(int id)
        {
            return mapper.Map<ResultCategoryDto>(await repository.Get((Category c) => c.Category_Id == id).FirstOrDefaultAsync());
        }

        public async Task<(bool, Category, string?)> Post(CategoryDto productDto)
        {
            var product = mapper.Map<Category>(productDto);

            if (!await ProductExists(product.Name))
            {
                await repository.Create(product);
                return (true, product, null);
            }

            logger.LogWarning("Product exists");
            return (false, product, "Product exists");
        }

        public async Task<(bool, Category?, string?)> Put(int id, CategoryDto productDto)
        {
            var product = mapper.Map<Category>(productDto);

            if (!await ProductExists(product.Name, id))
            {
                product.Category_Id = id;

                await repository.Update(product);
                return (true, product, null);
            }

            logger.LogWarning("Product doesn't exists");
            return (false, product, "Product doesn't exists");
        }

        public async Task<bool> ProductExists(int id)
        {
            return await repository.Get((Category c) => c.Category_Id == id).AnyAsync();
        }

        public async Task<bool> ProductExists(string name, int? id = null)
        {
            if (id.HasValue)
            {
                return await repository.Get((Category c) => c.Name == name && c.Category_Id != id).AnyAsync();
            }

            return await repository.Get((Category c) => c.Name == name).AnyAsync();
        }
    }
}
