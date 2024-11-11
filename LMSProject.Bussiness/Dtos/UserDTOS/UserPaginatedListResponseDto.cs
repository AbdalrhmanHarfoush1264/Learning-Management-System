namespace LMSProject.Bussiness.Dtos.UserDTOS
{
    public class UserPaginatedListResponseDto : UpdateUserRequest
    {
        public List<string>? roles { get; set; } = null!;
    }
}
