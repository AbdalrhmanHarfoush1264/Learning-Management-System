namespace LMSProject.Bussiness.Dtos.AssignmentDTOS
{
    public class AssignmentResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string CourseName { get; set; } = null!;
        public DateOnly CreatedDate { get; set; }
    }
}
