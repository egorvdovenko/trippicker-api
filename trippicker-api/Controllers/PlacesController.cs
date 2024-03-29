﻿using trippicker_api.Interfaces.Services;
using trippicker_api.Models.Places;
using trippicker_api.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Invest.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlacesController(IPlaceService PlaceService)
        {
            _placeService = PlaceService;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<PlaceModel>> GetAll([FromQuery] List<int> tagsIds)
        {
            var places = await _placeService.GetAll(tagsIds);

            return places;
        }

        [HttpGet]
        [Route("list")]
        public async Task<PagedList<PlaceItem>> GetList([FromQuery] PageFilter request)
        {
            var places = await _placeService.GetList(request);

            return places;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<PlaceModel> Get(int id)
        {
            var places = await _placeService.Get(id);

            return places;
        }

        [HttpPost]
        [Route("")]
        public async Task<int> Create(SavePlaceRequest request)
        {
            var placeId = await _placeService.Create(request);

            return placeId;
		}

        [HttpPut]
        [Route("{id:int}")]
        public async Task Update(int id, SavePlaceRequest request)
        {
            await _placeService.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task Delete(int id)
        {
            await _placeService.Delete(id);
        }
    }
}
