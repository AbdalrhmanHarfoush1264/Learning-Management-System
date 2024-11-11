namespace LMSProject.Bussiness.Dtos.CertificateDTOS
{
    public class CertificateResponseDto
    {
        public int Id { get; set; }
        public DateOnly IssueDate { get; set; }
        public string CourseName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
    }
}
