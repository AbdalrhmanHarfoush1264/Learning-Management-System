using LMSProject.Bussiness.Dtos.SubmissionDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "student")]
    public class SubmissionController : ControllerBase
    {
        #region Fileds
        private readonly ISubmissionService _submissionService;
        #endregion

        #region Constructors
        public SubmissionController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetSubmissionsList()
        {
            var response = await _submissionService.GetSubmissionListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetSubmissionsPaginated([FromQuery] SubmissionPaginatedListRequest request)
        {
            var response = await _submissionService.GetSubmissionPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetSubmissionById([FromRoute] int Id)
        {
            var response = await _submissionService.GetSubmissionByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddSubmission([FromBody] AddSubmissionRequest request)
        {

            var response = await _submissionService.AddSubmissionAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateSubmissionById([FromBody] UpdateSubmissionRequest request)
        {
            var response = await _submissionService.UpdateSubmissionAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteSubmissionById([FromRoute] int Id)
        {
            var response = await _submissionService.DeleteSubmissionAsync(Id);

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
