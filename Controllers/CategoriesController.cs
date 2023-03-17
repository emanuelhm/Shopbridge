using Microsoft.AspNetCore.Mvc;
using ShopBridge.Application;
using ShopBridge.Application.Dtos.Category;
using ShopBridge.Domain.Models;
using ShopBridge.Domain.Services.Interfaces;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService CategoryService;

        public CategoriesController(ICategoryService _CategoryService)
        {
            this.CategoryService = _CategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] SearchDto seachDto)
        {
            return Ok(await CategoryService.Get(seachDto));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var Category = await CategoryService.Get(id);

            if (Category == null) return BadRequest();

            return Ok(Category);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromForm] CategoryDto CategoryDto)
        {
            var (isOk, _, response) = await CategoryService.Put(id, CategoryDto);

            if (isOk) return Ok();

            return BadRequest(response);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromForm] CategoryDto CategoryDto)
        {
            var (isOk, _, response) = await CategoryService.Post(CategoryDto);

            if (isOk) return Ok();

            return BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var (isOk, response) = await CategoryService.Delete(id);

            if (isOk) return Ok();

            return BadRequest(response);
        }
    }
}
