using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SubmissionDate { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public Grade grade { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
