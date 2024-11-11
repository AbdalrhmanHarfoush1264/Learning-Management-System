using LMSProject.Bussiness.Dtos.CertificateDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CertificateController : ControllerBase
    {
        #region Fileds
        private readonly ICertificateService _certificateService;
        #endregion

        #region Constructors
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetCertificationList()
        {
            var response = await _certificateService.GetCertificateListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetCertificationPaginated([FromQuery] CertificationPaginatedListRequest request)
        {
            var response = await _certificateService.GetCertificationPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCertificateById([FromRoute] int Id)
        {
            var response = await _certificateService.GetCertificateByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCertificate([FromBody] AddCertificateRequest request)
        {

            var response = await _certificateService.AddCertificateAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateCertificateById([FromBody] UpdateCertificateRequest request)
        {


            var response = await _certificateService.UpdateCertificateAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteCertificateById([FromRoute] int Id)
        {
            var response = await _certificateService.DeleteCertificateAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }
        #endregion
    }
}
