namespace LMSProject.Bussiness.Dtos.CourseDTOS
{
    public class UpdateCourseRequest
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Level { get; set; }
    }
}
