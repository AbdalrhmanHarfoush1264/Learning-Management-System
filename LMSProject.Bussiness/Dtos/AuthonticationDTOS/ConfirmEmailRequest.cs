namespace LMSProject.Bussiness.Dtos.AuthonticationDTOS
{
    public class ConfirmEmailRequest
    {
        public int userId { get; set; }
        public string code { get; set; } = null!;
    }
}
