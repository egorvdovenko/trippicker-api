using System;
using trippicker_api.Interfaces;

namespace trippicker_api.Entities
{
    public class RefreshTokenEntity : ICreatedUtcDateTimeEntity, IUpdatedUtcDateTimeEntity, IDeletedUtcDateTimeEntity
    {
        public Guid Token { get; set; }
        public string JwtId { get; set; }
        public DateTime ExpiryUtcDateTime { get; set; }
        public bool Invalid { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime? DeletedUtcDateTime { get; set; }
        public DateTime UpdatedUtcDateTime { get; set; }
        public DateTime CreatedUtcDateTime { get; set; }
    }
}
