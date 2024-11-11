namespace LMSProject.Bussiness.Dtos.EnrollmentDTOS
{
    public class EnrollmentResponseDto
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public DateOnly EnrollmentDate { get; set; }
    }
}
