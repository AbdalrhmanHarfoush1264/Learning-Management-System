namespace LMSProject.Data.Data.Enities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }


        public ICollection<Lesson>? Lessons { get; set; } = new HashSet<Lesson>();

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
