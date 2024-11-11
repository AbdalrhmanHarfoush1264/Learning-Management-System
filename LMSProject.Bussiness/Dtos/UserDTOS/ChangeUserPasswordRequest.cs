namespace LMSProject.Bussiness.Dtos.UserDTOS
{
    public class ChangeUserPasswordRequest
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
