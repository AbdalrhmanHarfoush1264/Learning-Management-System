using LMSProject.Data.Data.Enities.Identities;

namespace LMSProject.Data.Data.Enities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime SendDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
