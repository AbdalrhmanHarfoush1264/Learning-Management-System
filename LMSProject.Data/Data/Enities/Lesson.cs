namespace LMSProject.Data.Data.Enities
{
    public class Lesson
    {

        public int LessonId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
    }
}
