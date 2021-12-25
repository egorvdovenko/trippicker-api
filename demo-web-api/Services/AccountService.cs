using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using trippicker_api.Configuration;
using trippicker_api.Entities;
using trippicker_api.Enums;
using trippicker_api.Interfaces.Repositories;
using trippicker_api.Interfaces.Services;
using trippicker_api.Models.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace trippicker_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TrippickerApiConfiguration _config;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountService(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            TokenValidationParameters tokenValidationParameters,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<TrippickerApiConfiguration> config,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
            _roleManager = roleManager;
        }

        public async Task Register(RegistrationData data)
        {
            await CreateUser(data, isConfirmed: false);
        }

        private async Task<UserEntity> CreateUser(RegistrationData data, bool isConfirmed)
        {
            var user = new UserEntity
            {
                Email = data.Email,
                UserName = data.Email,
                Confirmed = isConfirmed,
            };

            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
                throw new Exception();

            var model = new UserModel(user);

            return user;
        }

        public async Task<AuthenticationResult> Login(LoginData data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email);
            if (user == null)
                throw new Exception("UserNotFound");

            var roles = await _userManager.GetRolesAsync(user);

            var result = await _signInManager.PasswordSignInAsync(user, data.Password, true, false);
            if (!result.Succeeded)
                throw new Exception(/*ErrorCode.IncorrectPassword*/);

            return await CreateAuthenticationResult(user, roles);
        }

        public async Task<AuthenticationResult> RefreshToken(string token, Guid refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                throw new Exception(/*ErrorCode.IncorrectJwt*/);
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedToken = await _refreshTokenRepository.GetToken(refreshToken);

            if (storedToken == null)
                throw new Exception(/*ErrorCode.RefreshTokenNotFound*/);

            if (storedToken.ExpiryUtcDateTime < DateTime.UtcNow)
                throw new Exception(/*ErrorCode.RefreshTokenExpired*/);

            if (storedToken.Invalid)
                throw new Exception(/*ErrorCode.InvalidRefreshToken*/);

            if (storedToken.JwtId != jti)
                throw new Exception(/*ErrorCode.IncorrectJwtId*/);

            storedToken.Invalid = true;
            await _refreshTokenRepository.Update(storedToken);
            var userEmail = validatedToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var roles = await _userManager.GetRolesAsync(user);

            return await CreateAuthenticationResult(user, roles);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = _tokenValidationParameters.Clone();
                validationParameters.ValidateLifetime = false;
                var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
                if (!IsValidAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsValidAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<AuthenticationResult> CreateAuthenticationResult(UserEntity user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_config.SecurityKey);

            var claims = new List<Claim>
            {
                new Claim(Claims.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                var r = await _roleManager.FindByNameAsync(role);
                var roleClaims = await _roleManager.GetClaimsAsync(r);

                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.AddRange(roleClaims);
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Subject = claimsIdentity
            };
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            var refreshToken = new RefreshTokenEntity
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreatedUtcDateTime = DateTime.UtcNow,
                Token = Guid.NewGuid(),
                ExpiryUtcDateTime = DateTime.UtcNow.AddDays(7),
                Invalid = false
            };

            await _refreshTokenRepository.Add(refreshToken);

            var authResult = new AuthenticationResult
            {
                Token = jwt,
                RefreshToken = refreshToken.Token
            };

            return authResult;
        }

        public async Task Logout(int userId)
        {
            await _signInManager.SignOutAsync();
            await _refreshTokenRepository.RemoveToken(userId);
        }
    }
}
