using System;
using System.Linq;
using System.Threading.Tasks;
using trippicker_api.Entities;
using trippicker_api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace trippicker_api.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly TrippickerDbContext _db;

        public RefreshTokenRepository(TrippickerDbContext db)
        {
            _db = db;
        }

        public async Task Add(RefreshTokenEntity entity)
        {
            await _db.RefreshTokens.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshTokenEntity> GetToken(Guid token)
        {
            return await _db.RefreshTokens.SingleAsync(x => x.Token == token);
        }

        public async Task Update(RefreshTokenEntity entity)
        {
            _db.RefreshTokens.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveToken(int userId)
        {
            var entity = await _db.RefreshTokens
                .OrderByDescending(x => x.CreatedUtcDateTime)
                .FirstAsync(x => x.UserId == userId && x.Invalid == false); // TODO: удалять конкретный токен, а не последний у пользователя

            entity.Invalid = true;
            await _db.SaveChangesAsync();
        }
    }
}
