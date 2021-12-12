using System;
using System.Linq;
using System.Security.Claims;
using demo_web_api.Enums;

namespace demo_web_api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims
                .Where(c => c.Type == Claims.UserId)
                .Select(c => int.Parse(c.Value))
                .First();
    }
}
