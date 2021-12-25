using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using trippicker_api.Extensions;
using trippicker_api.Models.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using trippicker_api.Interfaces.Services;

namespace trippicker_api.Controllers
{
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task Register([FromBody] RegistrationData request)
        {
            await _accountService.Register(request);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthenticationResult> Login([FromBody] LoginData request)
        {
            var authResult = await _accountService.Login(request);
            return authResult;
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResult = await _accountService.RefreshToken(request.Token, request.RefreshToken);
            return new OkObjectResult(authResult);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.GetId();
            await _accountService.Logout(userId);
            return Ok();
        }
    }
}
