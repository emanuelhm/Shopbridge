using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Application.Dtos.Product;
using ShopBridge.Domain.Models;
using ShopBridge.Data;
using ShopBridge.Data.Repository;
using ShopBridge.Domain.Services.Interfaces;
using ShopBridge.Application;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using dotenv.net;
using ShopBridge.Application.Services.Interfaces;

namespace ShopBridge.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly IImageUploadService imageUploadService;

        public ProductService(Shopbridge_Context _context, ILogger<ProductService> _logger, IMapper _mapper, IImageUploadService _imageUploadService)
        {
            repository = new Repository(_context);
            logger = _logger;
            mapper = _mapper;
            imageUploadService = _imageUploadService;
        }

        public async Task<(bool, string?)> Delete(int id)
        {
            if (await ProductExists(id))
            {
                var product = await repository.Get((Product p) => p.Product_Id == id).FirstOrDefaultAsync();

                if (product != null)
                {
                    await repository.Delete(product);
                    return (true, null);
                }
            }

            logger.LogWarning("Product doesn't exists");
            return (false, "Product doesn't exists");
        }

        public async Task<IEnumerable<ResultProductDto>> Get(SearchDto searchDto)
        {
            var query = repository.Get<Product>((p) => p.Categories, (p) => p.Tags);

            if (searchDto.HasPagination())
            {
                query = query.Skip(searchDto.Skip()).Take(searchDto.Size ?? 0);
            }

            if (!string.IsNullOrEmpty(searchDto.Text))
            {
                query = query.Where(c => 
                    c.Name.Contains(searchDto.Text) ||
                    c.Tags.Any(t => t.Name.Contains(searchDto.Text)) ||
                    c.Categories.Any(t => t.Name.Contains(searchDto.Text))
                );
            }

            return mapper.Map<ResultProductDto[]>(await query.ToListAsync());
        }

        public async Task<ResultProductDto?> Get(int id)
        {
            return mapper.Map<ResultProductDto>(
                await repository
                    .Get((Product p) => p.Product_Id == id, (p) => p.Categories, (p) => p.Tags)
                    .FirstOrDefaultAsync()
            );
        }

        public async Task<(bool, Product?, string?)> Post(ProductDto productDto)
        {
            if (await ProductExists(productDto.Name))
            {
                logger.LogWarning("Product exists");
                return (false, null, "Product exists");
            }

            string? response = null;

            var product = mapper.Map<Product>(productDto);

            foreach (var tagId in productDto.Tags)
            {
                var tag = await repository.Get((Tag t) => t.Tag_Id == tagId).FirstAsync();
                product.Tags.Add(tag);
            }

            foreach (var categoryId in productDto.Categories)
            {
                var category = await repository.Get((Category c) => c.Category_Id == categoryId).FirstAsync();
                product.Categories.Add(category);
            }

            if (productDto.Image != null)
            {
                var uploadResult = await imageUploadService.Upload(productDto.Image.FileName, productDto.Image.OpenReadStream());

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    product.PictureUrl = uploadResult.SecureUrl.AbsoluteUri;
                else
                    response = "Somenthing went wrong while trying to save your image, please try again later";
            }

            await repository.Create(product);
            return (true, product, response);
        }

        public async Task<(bool, Product?, string?)> Put(int id, ProductDto productDto)
        {
            if (await ProductExists(productDto.Name, id))
            {
                logger.LogWarning("Product doesn't exists");
                return (false, null, "Product doesn't exists");
            }

            string? response = null;

            var product = mapper.Map<Product>(productDto);

            if (productDto.Image != null)
            {
                var uploadResult = await imageUploadService.Upload(productDto.Image.FileName, productDto.Image.OpenReadStream());

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    product.PictureUrl = uploadResult.SecureUrl.AbsoluteUri;
                else
                    response = "Somenthing went wrong while trying to save your image, please try again later";
            }

            product.Product_Id = id;

            await repository.Update(product);
            return (true, product, response);
        }

        public async Task<bool> ProductExists(int id)
        {
            return await repository.Get((Product p) => p.Product_Id == id).AnyAsync();
        }

        public async Task<bool> ProductExists(string name, int? id = null)
        {
            if (id.HasValue)
            {
                return await repository.Get((Product p) => p.Name == name && p.Product_Id != id).AnyAsync();
            }

            return await repository.Get((Product p) => p.Name == name).AnyAsync();
        }
    }
}
