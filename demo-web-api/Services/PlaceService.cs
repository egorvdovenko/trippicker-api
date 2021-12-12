using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Entities.ManyToMany;
using demo_web_api.Extensions;
using demo_web_api.Interfaces.Services;
using demo_web_api.Models.Places;
using demo_web_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace demo_web_api.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly DemoDbContext _db;

        public PlaceService(DemoDbContext db)
        {
            _db = db;
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
                    TagsIds = p.PlaceTags
                        .Where(pt => pt.PlaceId == p.Id)
                        .Select(pt => pt.TagId)
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
                PlaceTags = request.TagsIds
                    .Select(tid => new PlaceTagEntity
				    {
                        TagId = tid
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
            place.PlaceTags = request.TagsIds
                .Select(tid => new PlaceTagEntity
                {
                    PlaceId = id,
                    TagId = tid
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
