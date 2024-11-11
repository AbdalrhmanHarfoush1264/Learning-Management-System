using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.AuthonticationDTOS;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ICusAuthonticationService
    {
        public Task<CusResponse<JwtAuthResponse>> SignIn(SignInRequest request);
        public Task<CusResponse<JwtAuthResponse>> RefreshToken(RefreshTokenRequest request);
        public Task<CusResponse<string>> IsValidToken(string accessToken);
        public Task<CusResponse<string>> ConfirmEmailAsync(ConfirmEmailRequest request);
    }
}
