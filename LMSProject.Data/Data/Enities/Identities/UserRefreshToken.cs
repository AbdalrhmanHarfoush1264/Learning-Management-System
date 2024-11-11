namespace LMSProject.Data.Data.Enities.Identities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public User? User { get; set; }
    }
}
