using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public DateTime IssueDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
