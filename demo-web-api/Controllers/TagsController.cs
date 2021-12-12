using demo_web_api.Interfaces.Services;
using demo_web_api.Models;
using demo_web_api.Models.Tags;
using demo_web_api.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invest.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService TagService)
        {
            _tagService = TagService;
        }

        [HttpGet]
        [Route("")]
        public async Task<PagedList<TagItem>> GetList([FromQuery] PageFilter request)
        {
            var tags = await _tagService.GetList(request);

            return tags;
        }

        [HttpGet]
        [Route("listItems")]
        public async Task<List<ListItem<int>>> GetListItems()
        {
            return await _tagService.GetListItems();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<TagModel> Get(int id)
        {
            var tags = await _tagService.Get(id);

            return tags;
        }

        [HttpPost]
        [Route("")]
        public async Task<int> Create(SaveTagRequest request)
        {
            var tagId = await _tagService.Create(request);

            return tagId;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task Update(int id, SaveTagRequest request)
        {
            await _tagService.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(int id)
        {
            await _tagService.Delete(id);
        }
    }
}
