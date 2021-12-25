using System;
using System.Threading.Tasks;
using trippicker_api.Entities;

namespace trippicker_api.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetToken(Guid token);
        Task Update(RefreshTokenEntity entity);
        Task Add(RefreshTokenEntity entity);
        Task RemoveToken(int userId);
    }
}
