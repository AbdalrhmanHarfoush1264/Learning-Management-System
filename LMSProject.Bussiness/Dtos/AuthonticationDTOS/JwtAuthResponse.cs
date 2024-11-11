namespace LMSProject.Bussiness.Dtos.AuthonticationDTOS
{
    public class JwtAuthResponse
    {
        public string AccessToken { get; set; } = null!;
        public RefreshToken RefreshToken { get; set; } = null!;
    }

    public class RefreshToken
    {
        public string UserName { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public string refreshTokenString { get; set; } = null!;
    }
}
