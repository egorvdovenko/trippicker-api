using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;
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
                .Select(c => new PlaceItem
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToPagedListAsync(pageFilter);

            return places;
        }

        public async Task<PlaceModel> Get(int id)
        {
            var place = await _db.Places
                .Select(c => new PlaceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstAsync(c => c.Id == id);

            return place;
        }

        public async Task<int> Create(SavePlaceRequest request)
        {
            var place = new PlaceEntity
            {
                Name = request.Name,
                Description = request.Description
            };

            await _db.AddAsync(place);
            await _db.SaveChangesAsync();

            return place.Id;
        }

        public async Task Update(int id, SavePlaceRequest request)
        {
            var place = await _db.Places
                .FirstAsync(c => c.Id == id);

            place.Name = request.Name;
            place.Description = request.Description;

            _db.Update(place);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var place = await _db.Places
                .SingleAsync(x => x.Id == id);

            _db.Remove(place);

            await _db.SaveChangesAsync();
        }
    }
}
