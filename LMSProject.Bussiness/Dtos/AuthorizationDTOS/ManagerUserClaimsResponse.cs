namespace LMSProject.Bussiness.Dtos.AuthorizationDTOS
{
    public class ManagerUserClaimsResponse
    {
        public int userId { get; set; }
        public List<UserClaims>? UserClaims { get; set; }
    }

    public class UserClaims
    {
        public string Type { get; set; } = null!;
        public bool Value { get; set; }
    }
}
