using Microsoft.AspNetCore.Http;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IVideoService
    {
        public Task<string?> UploadVideoAsync(IFormFile? videoFile);
        public bool DeleteVideo(string videoUrl);
    }
}
