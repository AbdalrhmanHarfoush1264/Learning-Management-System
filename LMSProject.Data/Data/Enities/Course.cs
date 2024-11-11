using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Level { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Module>? Modules { get; set; } = new HashSet<Module>();
        public ICollection<Forum>? Forums { get; set; } = new HashSet<Forum>();
        public ICollection<Certificate>? Certificates { get; set; } = new HashSet<Certificate>();
        public ICollection<Enrollment>? Enrollments { get; set; } = new HashSet<Enrollment>();
        public ICollection<Assignment>? Assignments { get; set; } = new HashSet<Assignment>();

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
