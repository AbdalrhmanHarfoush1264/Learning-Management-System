using LMSProject.Bussiness.Dtos.AuthonticationDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthonticationController : ControllerBase
    {
        private readonly ICusAuthonticationService _cusAuthonticationService;

        public AuthonticationController(ICusAuthonticationService cusAuthonticationService)
        {
            _cusAuthonticationService = cusAuthonticationService;
        }

        [HttpPost("Sign-In")]
        public async Task<IActionResult> SignIn([FromForm] SignInRequest request)
        {
            var response = await _cusAuthonticationService.SignIn(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest request)
        {
            var response = await _cusAuthonticationService.RefreshToken(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Is-Valid-Token")]
        public async Task<IActionResult> IsValidToken([FromQuery] string accessToken)
        {
            var response = await _cusAuthonticationService.IsValidToken(accessToken);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.Unauthorized)
                return Unauthorized(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var response = await _cusAuthonticationService.ConfirmEmailAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
