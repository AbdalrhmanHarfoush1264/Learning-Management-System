namespace LMSProject.Bussiness.Dtos.SubmissionDTOS
{
    public class AddSubmissionRequest
    {
        public string Content { get; set; } = null!;
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
    }
}
