using System.Collections.Generic;
using System.Threading.Tasks;
using trippicker_api.Models.Places;
using trippicker_api.Pagination;

namespace trippicker_api.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<List<PlaceModel>> GetAll();
        Task<PagedList<PlaceItem>> GetList(PageFilter request);
        Task<PlaceModel> Get(int PlaceId);
        public Task<int> Create(SavePlaceRequest request);
        public Task Update(int id, SavePlaceRequest request);
        public Task Delete(int id);
    }
}
