using LMSProject.Bussiness.Dtos.LessonDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonByIdAsync(int id)
        {
            var response = await _lessonService.GetLessonByIdAsync(id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddLessonAsync([FromForm] AddLessonRequest request)
        {
            var response = await _lessonService.AddLessonAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLessonAsync([FromForm] UpdateLessonRequest request)
        {
            var response = await _lessonService.UpdateLessonAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLessonAsync(int id)
        {
            var response = await _lessonService.DeleteLessonAsync(id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedLessonsAsync([FromQuery] LessonPaginatedListRequest request)
        {
            var response = await _lessonService.GetLessonPaginatedListAsync(request);

            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }
    }
}
