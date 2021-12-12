using System;
using System.Threading.Tasks;
using demo_web_api.Entities;

namespace demo_web_api.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity> GetToken(Guid token);
        Task Update(RefreshTokenEntity entity);
        Task Add(RefreshTokenEntity entity);
        Task RemoveToken(int userId);
    }
}
