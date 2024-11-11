using LMSProject.Bussiness.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IEmailService
    {
        public Task<CusResponse<string>> SendEmailAsync(string Email, string Message);
    }
}
