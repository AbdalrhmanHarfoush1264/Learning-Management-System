using LMSProject.Bussiness.Dtos.GradeDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GradeController : ControllerBase
    {
        #region Fileds
        private readonly IGradeService _gradeService;
        #endregion

        #region Constructors
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetGradeList()
        {
            var response = await _gradeService.GetGradeListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetGradePaginated([FromQuery] GradePaginatedListRequest request)
        {
            var response = await _gradeService.GetGradePaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetGradeById([FromRoute] int Id)
        {
            var response = await _gradeService.GetGradeByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddGrade([FromBody] AddGradeRequest request)
        {

            var response = await _gradeService.AddGradeAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateGradeById([FromBody] UpdateGradeRequest request)
        {

            var response = await _gradeService.UpdateGradeAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteGradeById([FromRoute] int Id)
        {
            var response = await _gradeService.DeleteGradeAsync(Id);

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
