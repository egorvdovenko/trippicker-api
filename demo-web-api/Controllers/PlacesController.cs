using demo_web_api.Interfaces.Services;
using demo_web_api.Models.Places;
using demo_web_api.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Invest.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _PlaceService;

        public PlacesController(IPlaceService PlaceService)
        {
            _PlaceService = PlaceService;
        }

        [HttpGet]
        [Route("")]
        public async Task<PagedList<PlaceItem>> GetList([FromQuery] PageFilter request)
        {
            var places = await _PlaceService.GetList(request);

            return places;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<PlaceModel> Get(int id)
        {
            var places = await _PlaceService.Get(id);

            return places;
        }

        [HttpPost]
        [Route("")]
        public async Task<int> Create(SavePlaceRequest request)
        {
            var placeId = await _PlaceService.Create(request);

            return placeId;
		}

        [HttpPut]
        [Route("{id:int}")]
        public async Task Update(int id, SavePlaceRequest request)
        {
            await _PlaceService.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(int id)
        {
            await _PlaceService.Delete(id);
        }
    }
}
