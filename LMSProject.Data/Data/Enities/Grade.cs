namespace LMSProject.Data.Data.Enities
{
    public class Grade
    {
        public int GradeId { get; set; }
        public float grade { get; set; }


        public int SubmissionId { get; set; }
        public Submission Submission { get; set; } = null!;
    }
}
