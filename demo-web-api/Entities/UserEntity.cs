using System;
using System.Collections.Generic;
using demo_web_api.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace demo_web_api.Entities
{
    public class UserEntity : IdentityUser<int>, ICreatedUtcDateTimeEntity, IUpdatedUtcDateTimeEntity, IDeletedUtcDateTimeEntity
    {
        public bool Confirmed { get; set; }
        public List<RefreshTokenEntity> RefreshTokens { get; set; } = new ();
        public DateTime CreatedUtcDateTime { get; set; }
        public DateTime UpdatedUtcDateTime { get; set; }
        public DateTime? DeletedUtcDateTime { get; set; }
    }
}
