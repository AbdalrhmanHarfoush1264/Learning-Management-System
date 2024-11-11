namespace LMSProject.Bussiness.Dtos.UserDTOS
{
    public class UserResponseDto : UpdateUserRequest
    {
        public List<string>? roles { get; set; } = null!;
    }
}
