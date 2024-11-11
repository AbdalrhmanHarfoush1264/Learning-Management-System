namespace LMSProject.Bussiness.Dtos.NotificationDTOS
{
    public class AddNotificationRequest
    {
        public string Message { get; set; } = null!;
        public int studentId { get; set; }
    }
}
