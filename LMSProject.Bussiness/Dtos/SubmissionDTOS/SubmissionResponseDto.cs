namespace LMSProject.Bussiness.Dtos.SubmissionDTOS
{
    public class SubmissionResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateOnly SubmissionDate { get; set; }
        public string AssignmentName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
    }
}
