namespace LMSProject.Bussiness.Dtos.ModuleDTOS
{
    public class ModuleResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string CourseName { get; set; } = null!;
    }
}
