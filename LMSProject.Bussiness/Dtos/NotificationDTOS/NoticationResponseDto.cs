namespace LMSProject.Bussiness.Dtos.NotificationDTOS
{
    public class NoticationResponseDto
    {
        public int Id { get; set; }
        public string message { get; set; } = null!;
        public DateOnly date { get; set; }
        public string StudentName { get; set; } = null!;
    }
}
