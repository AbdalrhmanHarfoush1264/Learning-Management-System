using LMSProject.Bussiness.Dtos.ForumPostDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ForumPostController : ControllerBase
    {
        #region Fileds
        private readonly IForumPostService _forumPostService;
        #endregion

        #region Constructors
        public ForumPostController(IForumPostService forumPostService)
        {
            _forumPostService = forumPostService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetForumPostList()
        {
            var response = await _forumPostService.GetForumPostListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetForumPostPaginated([FromQuery] ForumPostPaginatedListRequest request)
        {
            var response = await _forumPostService.GetForumPostPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetForumPostById([FromRoute] int Id)
        {
            var response = await _forumPostService.GetForumPostByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddForumPost([FromBody] AddForumPostRequest request)
        {

            var response = await _forumPostService.AddForumPostAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateForumPostById([FromBody] UpdateForumPostRequest request)
        {
            var response = await _forumPostService.UpdateForumPostAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteForumPostById([FromRoute] int Id)
        {
            var response = await _forumPostService.DeleteForumPostAsync(Id);

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
