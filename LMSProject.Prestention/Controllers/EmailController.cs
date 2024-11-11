using LMSProject.Bussiness.Dtos.EmailDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        #region Fileds
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        #endregion

        #region Methods

        #endregion

        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailRequest request)
        {
            var response = await _emailService.SendEmailAsync(request.Email, request.Message);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
