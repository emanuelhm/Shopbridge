using CloudinaryDotNet;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Application;
using ShopBridge.Application.Dtos.Product;
using ShopBridge.Domain.Models;
using ShopBridge.Domain.Services.Interfaces;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]   
        public async Task<IActionResult> GetProduct([FromQuery] SearchDto searchDto)
        {
            return Ok(await productService.Get(searchDto));
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await productService.Get(id);

            if (product == null) return BadRequest();

            return Ok(product);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductDto productDto)
        {
            var (isOk, _, response) = await productService.Put(id, productDto);

            if (isOk) return Ok();

            return BadRequest(response);
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDto productDto)
        {
            var (isOk, _, response) = await productService.Post(productDto);

            if (isOk) return Ok(response);

            return BadRequest(response);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var (isOk, response) = await productService.Delete(id);

            if (isOk) return Ok();

            return BadRequest(response);
        }
    }
}
