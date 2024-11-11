using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class EmailService : IEmailService
    {
        #region Fileds
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructors
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Functions

        public async Task<CusResponse<string>> SendEmailAsync(string Email, string Message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "welcome"
                    };

                    var message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };

                    message.From.Add(new MailboxAddress("Learning Mangement System", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("To :", Email));
                    message.Subject = "new Contact Submitted Data.";
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return new CusResponse<string>
                {
                    IsSuccess = true,
                    Message = "Message Send Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region privare-Methods
        private CusResponse<string> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new CusResponse<string>
            {
                IsSuccess = false,
                Message = message,
                Data = null,
                DataCount = 0,
                StatusCode = statusCode
            };
        }

        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An error occurred: " + string.Join(", ", errors.Select(e => e.Description));
        }
        #endregion
    }
}
