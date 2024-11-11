namespace LMSProject.Bussiness.Dtos.LessonDTOS
{
    public class LessonResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public string ModuleName { get; set; } = null!;
    }
}
