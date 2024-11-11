namespace LMSProject.Bussiness.Dtos.CourseDTOS
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Level { get; set; }
        public DateOnly CreatedTime { get; set; }
        public string TeacherName { get; set; } = null!;
    }
}
