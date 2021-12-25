using System;

namespace trippicker_api.Models.Account
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
