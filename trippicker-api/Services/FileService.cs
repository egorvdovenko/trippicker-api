using System.Threading.Tasks;
using trippicker_api.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using trippicker_api.Models.Files;

namespace trippicker_api.Services
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public FileService()
        {
            var account = new Account("dncyoozwj", "347425183227223", "Y9ZxODCoPVGLJj2OWiF4YHk9QVw");
            _cloudinary = new Cloudinary(account);
        }

		public Task<FileItem> Download()
		{
			throw new System.NotImplementedException();
		}

		public async Task<FileItem> Upload(IFormFile file)
		{
            var bytes = GetBytes(file);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, new MemoryStream(bytes))
            };

            var result = await _cloudinary.UploadAsync(uploadParams);


			return new FileItem
			{
                Id = result.PublicId,
                Name = result.OriginalFilename,
				Url = result.Url
			};
		}

        private byte[] GetBytes(IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
