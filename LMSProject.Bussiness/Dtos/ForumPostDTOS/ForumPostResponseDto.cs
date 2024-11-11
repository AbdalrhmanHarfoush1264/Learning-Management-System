namespace LMSProject.Bussiness.Dtos.ForumPostDTOS
{
    public class ForumPostResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateOnly PostDate { get; set; }
        public string ForumTitle { get; set; } = null!;
        public string StudentName { get; set; } = null!;
    }
}
