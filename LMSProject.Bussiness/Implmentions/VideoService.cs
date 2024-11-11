using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LMSProject.Bussiness.Implmentions
{
    public class VideoService : IVideoService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public VideoService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        #endregion

        #region Functions

        public async Task<string?> UploadVideoAsync(IFormFile? videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
                return null;

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "videos");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + videoFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(fileStream);
            }

            string baseUrl = _configuration.GetValue<string>("AppSettings:BaseUrl");
            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = _webHostEnvironment.IsDevelopment() ? "https://localhost:7264" : "https://yourdomain.com";
            }

            return $"{baseUrl}/videos/{uniqueFileName}";
        }


        public bool DeleteVideo(string videoUrl)
        {
            if (string.IsNullOrEmpty(videoUrl))
                return false;

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, videoUrl.Replace("https://localhost:7264/", "").TrimStart('/'));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        #endregion
    }
}
