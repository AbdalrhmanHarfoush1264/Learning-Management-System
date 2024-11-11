namespace LMSProject.Bussiness.Dtos.AssignmentDTOS
{
    public class AddAssignmentRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int CourseId { get; set; }
    }
}
