using System.Linq;
using System.Threading.Tasks;
using trippicker_api.Entities;
using trippicker_api.Entities.ManyToMany;
using trippicker_api.Extensions;
using trippicker_api.Interfaces.Services;
using trippicker_api.Models.Places;
using trippicker_api.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using trippicker_api.Models.Files;

namespace trippicker_api.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly TrippickerDbContext _db;

        public PlaceService(TrippickerDbContext db)
        {
            _db = db;
        }

        public async Task<List<PlaceModel>> GetAll(List<int> tagsIds)
        {
            var query = _db.Places.AsNoTracking();

            if (tagsIds.Any())
			{
                query = query.Where(t => tagsIds.Contains(t.Id));
            }

			var places = await query
				.Select(p => new PlaceModel
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Latitude = p.Latitude,
					Longitude = p.Longitude,
					TagsIds = p.PlaceTags
						.Where(pt => pt.PlaceId == p.Id)
						.Select(pt => pt.TagId)
						.ToList(),
                    Images = p.Images
                        .Select(i => new FileItem { 
                            Id = i.Id, 
                            Name = i.Name, 
                            Url = i.Url 
                        })
                        .ToList()
				})
				.ToListAsync();

			return places;
        }

        public async Task<PagedList<PlaceItem>> GetList(PageFilter pageFilter)
        {
            var places = await _db.Places
                .AsNoTracking()
                .Select(p => new PlaceItem
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToPagedListAsync(pageFilter);

            return places;
        }

        public async Task<PlaceModel> Get(int id)
        {
            var place = await _db.Places
                .Select(p => new PlaceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    TagsIds = p.PlaceTags
                        .Where(pt => pt.PlaceId == p.Id)
                        .Select(pt => pt.TagId)
                        .ToList(),
                    Images = p.Images
                        .Select(i => new FileItem { 
                            Id = i.Id, 
                            Name = i.Name, 
                            Url = i.Url 
                        })
                        .ToList()
                })
                .FirstAsync(p => p.Id == id);

            return place;
        }

        public async Task<int> Create(SavePlaceRequest request)
        {
            var place = new PlaceEntity
            {
                Name = request.Name,
                Description = request.Description,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PlaceTags = request.TagsIds
                    .Select(tid => new PlaceTagEntity
				    {
                        TagId = tid
				    })
                    .ToList(),
                Images = request.Images
                    .Select(i => new FileEntity {
                        Id = i.Id,
                        Name = i.Name,
                        Url = i.Url
                    })
                    .ToList()
            };

            await _db.AddAsync(place);
            await _db.SaveChangesAsync();

            return place.Id;
        }

        public async Task Update(int id, SavePlaceRequest request)
        {
            var place = await _db.Places
                .Include(p => p.PlaceTags)
                .FirstAsync(p => p.Id == id);

            _db.RemoveRange(place.PlaceTags);

            place.Name = request.Name;
            place.Description = request.Description;
            place.Latitude = request.Latitude;
            place.Longitude = request.Longitude;
            place.PlaceTags = request.TagsIds
                .Select(tid => new PlaceTagEntity
                {
                    PlaceId = id,
                    TagId = tid
                })
                .ToList();
            place.Images = request.Images
                .Select(i => new FileEntity
                {
                    Id = i.Id,
                    Name = i.Name,
                    Url = i.Url
                })
                .ToList();

            _db.Update(place);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var place = await _db.Places
                .SingleAsync(p => p.Id == id);

            _db.Remove(place);

            await _db.SaveChangesAsync();
        }
    }
}
