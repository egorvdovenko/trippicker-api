using System.Threading.Tasks;
using demo_web_api.Models.Places;
using demo_web_api.Pagination;

namespace demo_web_api.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<PlaceModel> Get(int PlaceId);
        Task<PagedList<PlaceItem>> GetList(PageFilter request);
        public Task<int> Create(SavePlaceRequest request);
        public Task Update(int id, SavePlaceRequest request);
        public Task Delete(int id);
    }
}
