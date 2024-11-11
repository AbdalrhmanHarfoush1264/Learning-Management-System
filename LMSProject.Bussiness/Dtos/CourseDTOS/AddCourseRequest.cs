namespace LMSProject.Bussiness.Dtos.CourseDTOS
{
    public class AddCourseRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Level { get; set; }
        public int TeacherId { get; set; }
    }
}
