using System;

namespace demo_web_api.Models.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
