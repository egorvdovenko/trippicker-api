using demo_web_api.Interfaces;
using demo_web_api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace demo_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeachersController : ControllerBase
    {
        private readonly ILogger<TeachersController> _logger;
        private readonly ITeacherService _teacherService;

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        //[AllowAnonymous]
        [HttpPost]
        public async Task Add(AddTeacherRequest request)
        {
            await _teacherService.Add(request);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int teacherId)
        {
            var res = await _teacherService.Get(teacherId);

            return new OkObjectResult(res);
        }
    }
}
