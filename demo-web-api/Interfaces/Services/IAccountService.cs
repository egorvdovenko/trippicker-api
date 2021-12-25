using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trippicker_api.Entities;
using trippicker_api.Models.Account;

namespace trippicker_api.Interfaces.Services
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
