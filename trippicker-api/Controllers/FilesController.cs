using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using trippicker_api.Interfaces.Services;
using trippicker_api.Models.Files;

namespace trippicker_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService FileService)
        {
            _fileService = FileService;
        }

        [HttpPost]
        [Route("")]
        public async Task<FileItem> Upload(IFormFile file)
        {
            var fileItem = await _fileService.Upload(file);

            return fileItem;
        }
    }
}
