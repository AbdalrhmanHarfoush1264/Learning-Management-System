using LMSProject.Bussiness.Dtos.EnrollmentDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        #region Fileds
        private readonly IEnrollmentService _enrollmentService;
        #endregion

        #region Constructors
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetEnrollmentsList()
        {
            var response = await _enrollmentService.GetEnrollmentListAsync();
            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetEnrollmentsPaginated([FromQuery] EnrollmentPaginatedListRequest request)
        {
            var response = await _enrollmentService.GetEnrollmentPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetEnrollmentsById([FromRoute] int Id)
        {
            var response = await _enrollmentService.GetEnrollmentByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddEnrollment([FromBody] AddEnrollmentRequest request)
        {

            var response = await _enrollmentService.AddEnrollmentAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateEnrollmentById([FromBody] UpdateEnrollmentRequest request)
        {
            var response = await _enrollmentService.UpdateEnrollmentAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteEnrollmentById([FromRoute] int Id)
        {
            var response = await _enrollmentService.DeleteEnrollmentAsync(Id);

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
