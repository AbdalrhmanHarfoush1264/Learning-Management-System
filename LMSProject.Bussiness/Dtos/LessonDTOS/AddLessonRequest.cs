using Microsoft.AspNetCore.Http;

namespace LMSProject.Bussiness.Dtos.LessonDTOS
{
    public class AddLessonRequest
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int ModuleId { get; set; }
        public IFormFile? videoFile { get; set; }
    }
}
