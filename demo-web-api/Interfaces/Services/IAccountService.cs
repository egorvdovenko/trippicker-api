using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Models.Account;

namespace demo_web_api.Interfaces.Services
{
    public interface IAccountService
    {
        Task Register(RegistrationData data);
        Task<AuthenticationResult> Login(LoginData data);
        Task<AuthenticationResult> RefreshToken(string token, Guid refreshToken);
        Task Logout(int userId);
        Task<AuthenticationResult> CreateAuthenticationResult(UserEntity user, IList<string> roles);
    }
}
