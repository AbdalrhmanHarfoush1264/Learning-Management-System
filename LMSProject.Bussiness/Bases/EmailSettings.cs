namespace LMSProject.Bussiness.Bases
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string FromEmail { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
