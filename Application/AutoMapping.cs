using AutoMapper;
using ShopBridge.Application.Dtos.Category;
using ShopBridge.Application.Dtos.Product;
using ShopBridge.Application.Dtos.Tag;
using ShopBridge.Domain.Models;

namespace ShopBridge.Application
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDto, Product>()
                .ForMember((e) => e.Categories, (option) => option.Ignore())
                .ForMember((e) => e.Tags, (option) => option.Ignore());

            CreateMap<TagDto, Tag>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ResultProductDto>()
                .ForMember(e => e.Tags, options => options.MapFrom(source => source.Tags))
                .ForMember(e => e.Categories, options => options.MapFrom(source => source.Categories));

            CreateMap<Tag, ResultTagDto>();
            CreateMap<Category, ResultCategoryDto>();
        }
    }
}
