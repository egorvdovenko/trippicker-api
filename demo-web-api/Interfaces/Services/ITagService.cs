using System.Collections.Generic;
using System.Threading.Tasks;
using demo_web_api.Models;
using demo_web_api.Models.Tags;
using demo_web_api.Pagination;

namespace demo_web_api.Interfaces.Services
{
    public interface ITagService
    {
        Task<TagModel> Get(int TagId);
        Task<PagedList<TagItem>> GetList(PageFilter request);
        Task<List<ListItem<int>>> GetListItems();
        Task<int> Create(SaveTagRequest request);
        Task Update(int id, SaveTagRequest request);
        Task Delete(int id);
    }
}
