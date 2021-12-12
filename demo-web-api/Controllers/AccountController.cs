using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using demo_web_api.Extensions;
using demo_web_api.Interfaces;
using demo_web_api.Models.Account;
using demo_web_api.Models.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace demo_web_api.Controllers
{
    /// <summary>
    /// Действия с учётными записями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task Register([FromBody] RegistrationData request)
        {
            await _accountService.Register(request);
        }

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthenticationResult> Login([FromBody] LoginData request)
        {
            var authResult = await _accountService.Login(request);
            return authResult;
        }

        /// <summary>
        /// Обновление JWT-токена
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResult = await _accountService.RefreshToken(request.Token, request.RefreshToken);
            return new OkObjectResult(authResult);
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetId();
            await _accountService.Logout(userId);
            return Ok();
        }

        ///// <summary>
        ///// Получение профиля пользователя
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("profile")]
        //public async Task<IActionResult> GetProfile()
        //{
        //    var userId = User.GetId();
        //    var profile = await _accountService.GetUserProfile(userId);
        //    return new OkObjectResult(profile);
        //}

        //[HttpPost("profile")]
        //public async Task<IActionResult> SaveProfile([FromBody] SaveProfileRequest request)
        //{
        //    var userId = User.GetId();
        //    await _accountService.SaveUserProfile(userId, request);
        //    return Ok();
        //}
    }
}
