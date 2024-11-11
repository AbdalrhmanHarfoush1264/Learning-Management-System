namespace LMSProject.Bussiness.Dtos.GradeDTOS
{
    public class GradeResponseDto
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public int SubmissionId { get; set; }
        public string SubmissionContent { get; set; } = null!;
    }
}
