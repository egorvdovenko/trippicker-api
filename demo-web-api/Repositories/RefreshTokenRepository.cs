using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demo_web_api.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DemoDbContext _db;

        public RefreshTokenRepository(DemoDbContext db)
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
