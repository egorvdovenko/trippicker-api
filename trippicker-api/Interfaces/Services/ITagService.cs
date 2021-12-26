using System.Collections.Generic;
using System.Threading.Tasks;
using trippicker_api.Models;
using trippicker_api.Models.Tags;
using trippicker_api.Pagination;

namespace trippicker_api.Interfaces.Services
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
