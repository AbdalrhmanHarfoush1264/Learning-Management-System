namespace LMSProject.Bussiness.Dtos.ModuleDTOS
{
    public class AddModuleRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int CourseId { get; set; }
    }
}
