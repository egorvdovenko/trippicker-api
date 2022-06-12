using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using trippicker_api.Models.Files;

namespace trippicker_api.Interfaces.Services
{
    public interface IFileService
    {
        Task<FileItem> Upload (IFormFile file);
        Task<FileItem> Download ();
    }
}
