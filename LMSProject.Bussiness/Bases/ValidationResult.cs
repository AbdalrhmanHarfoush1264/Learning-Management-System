namespace LMSProject.Bussiness.Bases
{
    public enum ValidationResult
    {
        Success,
        AlgorithmIsWrong,
        TokenIsNotExpired,
        InvalidUserIdClaim,
        RefreshTokenNotFound,
        RefreshTokenExpired
    }
}
