﻿using System;
using System.Collections.Generic;
using demo_web_api.Models.Responses;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Models.Account;

namespace demo_web_api.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegistrationData data);
        Task<AuthenticationResult> Login(LoginData data);
        Task<AuthenticationResult> RefreshToken(string token, Guid refreshToken);
        Task<UserProfile> GetUserProfile(int userId);
        Task SaveUserProfile(int userId, SaveProfileRequest request);
        Task Logout(int userId);
        Task<AuthenticationResult> CreateAuthenticationResult(UserEntity user, IList<string> roles);
    }
}
