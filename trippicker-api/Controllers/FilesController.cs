using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace trippicker_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FilesController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, file.FileName);
				using Stream fileStream = new FileStream(filePath, FileMode.Create);
				await file.CopyToAsync(fileStream);
			}

            return Ok();
        }
    }
}
