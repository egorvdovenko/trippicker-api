using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Extensions;
using demo_web_api.Interfaces.Services;
using demo_web_api.Models;
using demo_web_api.Models.Tags;
using demo_web_api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace demo_web_api.Services
{
    public class TagService : ITagService
    {
        private readonly DemoDbContext _db;

        public TagService(DemoDbContext db)
        {
            _db = db;
        }

        public async Task<PagedList<TagItem>> GetList(PageFilter pageFilter)
        {
            var tags = await _db.Tags
                .AsNoTracking()
                .Select(c => new TagItem
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToPagedListAsync(pageFilter);

            return tags;
        }

        public Task<List<ListItem<int>>> GetListItems()
        {
            var listItems = _db.Tags
                .Select(x => new ListItem<int>
                {
                    Id = x.Id,
                    Text = x.Name
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

            return listItems;
        }

        public async Task<TagModel> Get(int id)
        {
            var tag = await _db.Tags
                .Select(c => new TagModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstAsync(c => c.Id == id);

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
                .FirstAsync(c => c.Id == id);

            tag.Name = request.Name;

            _db.Update(tag);

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _db.Tags
                .SingleAsync(x => x.Id == id);

            _db.Remove(tag);

            await _db.SaveChangesAsync();
        }
    }
}
