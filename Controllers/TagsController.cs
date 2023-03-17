using Microsoft.AspNetCore.Mvc;
using ShopBridge.Application;
using ShopBridge.Application.Dtos.Tag;
using ShopBridge.Domain.Models;
using ShopBridge.Domain.Services.Interfaces;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService TagService;

        public TagsController(ITagService _TagService)
        {
            this.TagService = _TagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTag([FromQuery] SearchDto searchDto)
        {
            return Ok(await TagService.Get(searchDto));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var Tag = await TagService.Get(id);

            if (Tag == null) return BadRequest();

            return Ok(Tag);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, [FromForm] TagDto TagDto)
        {
            var (isOk, _, response) = await TagService.Put(id, TagDto);

            if (isOk) return Ok();

            return BadRequest(response);
        }


        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag([FromForm] TagDto TagDto)
        {
            var (isOk, _, response) = await TagService.Post(TagDto);

            if (isOk) return Ok();

            return BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var (isOk, response) = await TagService.Delete(id);

            if (isOk) return Ok();

            return BadRequest(response);
        }
    }
}
