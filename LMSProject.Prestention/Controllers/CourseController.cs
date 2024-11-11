using LMSProject.Bussiness.Dtos.CourseDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {

        #region Fileds
        private readonly ICourseService _courseService;
        #endregion

        #region Constructors
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetCoursesList()
        {
            var response = await _courseService.GetCoursesListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetCoursesPaginated([FromQuery] CoursePaginatedListRequest request)
        {
            var response = await _courseService.GetCoursesPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int Id)
        {
            var response = await _courseService.GetCourseByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseRequest request)
        {

            var response = await _courseService.AddCourseAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPut()]
        public async Task<IActionResult> UpdateCourseById([FromBody] UpdateCourseRequest request)
        {


            var response = await _courseService.UpdateCourseAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int Id)
        {
            var response = await _courseService.DeleteCourseAsync(Id);

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
