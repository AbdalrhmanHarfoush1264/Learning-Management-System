using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public int CourserId { get; set; }
        public Course Course { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
