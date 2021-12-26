using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trippicker_api.Entities;
using trippicker_api.Extensions;
using trippicker_api.Interfaces.Services;
using trippicker_api.Models;
using trippicker_api.Models.Tags;
using trippicker_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace trippicker_api.Services
{
    public class TagService : ITagService
    {
        private readonly TrippickerDbContext _db;

        public TagService(TrippickerDbContext db)
        {
            _db = db;
        }

        public async Task<PagedList<TagItem>> GetList(PageFilter pageFilter)
        {
            var tags = await _db.Tags
                .AsNoTracking()
                .Select(t => new TagItem
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToPagedListAsync(pageFilter);

            return tags;
        }

        public Task<List<ListItem<int>>> GetListItems()
        {
            var listItems = _db.Tags
                .Select(t => new ListItem<int>
                {
                    Id = t.Id,
                    Text = t.Name
                })
                .OrderBy(t => t.Text)
                .ToListAsync();

            return listItems;
        }

        public async Task<TagModel> Get(int id)
        {
            var tag = await _db.Tags
                .Select(t => new TagModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .FirstAsync(t => t.Id == id);

            return tag;
        }

        public async Task<int> Create(SaveTagRequest request)
        {
            var tag = new TagEntity
            {
                Name = request.Name
            };

            await _db.AddAsync(tag);
            await _db.SaveChangesAsync();

            return tag.Id;
        }

        public async Task Update(int id, SaveTagRequest request)
        {
            var tag = await _db.Tags
                .FirstAsync(t => t.Id == id);

            tag.Name = request.Name;

            _db.Update(tag);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _db.Tags
                .SingleAsync(t => t.Id == id);

            _db.Remove(tag);

            await _db.SaveChangesAsync();
        }
    }
}
