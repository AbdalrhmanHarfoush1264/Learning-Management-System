using LMSProject.Bussiness.Dtos.UserDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {

        #region Fileds
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods

        [HttpGet]
        [Authorize(Policy = "GetUsersList")]
        public async Task<IActionResult> GetUsersList()
        {
            var response = await _userService.GetUserListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        [Authorize(Policy = "GetUsersList")]
        public async Task<IActionResult> GetUsersPaginated([FromQuery] UserPaginatedListRequest request)
        {
            var response = await _userService.GetUserPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        [Authorize(Policy = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int Id)
        {
            var response = await _userService.GetUserByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("add-admin")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "admin");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("add-student")]
        [AllowAnonymous]
        public async Task<IActionResult> AddStudent([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "student");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("add-teacher")]
        [AllowAnonymous]
        public async Task<IActionResult> AddTeacher([FromBody] AddUserRequest request)
        {
            var response = await _userService.AddUserAsync(request, "teacher");
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var response = await _userService.UpdateUserAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int Id)
        {
            var response = await _userService.DeleteUserAsync(Id);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Change-Password")]
        public async Task<IActionResult> ChanageUserPassword([FromBody] ChangeUserPasswordRequest request)
        {
            var response = await _userService.ChangePasswordAsync(request);
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
