using LMSProject.Bussiness.Dtos.AuthorizationDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        #region Fileds
        private readonly ICusAuthorizationService _cusAuthorizationService;
        #endregion

        #region Constructors
        public AuthorizationController(ICusAuthorizationService cusAuthorizationService)
        {
            _cusAuthorizationService = cusAuthorizationService;
        }
        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetRolesListAsync()
        {
            var response = await _cusAuthorizationService.GetRoleList();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] int Id)
        {
            var response = await _cusAuthorizationService.GetRoleById(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);


            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromForm] AddRoleRequest request)
        {
            var response = await _cusAuthorizationService.AddRole(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync([FromForm] UpdateRoleRequest request)
        {
            var response = await _cusAuthorizationService.UpdateRole(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] int Id)
        {
            var response = await _cusAuthorizationService.DeleteRole(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }


        [HttpGet("Manager-User-Roles/{userId:int}")]
        public async Task<IActionResult> ManagerUserRoles([FromRoute] int userId)
        {
            var response = await _cusAuthorizationService.ManagerUserRoles(userId);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Edit-User-Roles")]
        public async Task<IActionResult> UpdateManagerUserRoles([FromBody] ManagerUserRolesResponse request)
        {
            var response = await _cusAuthorizationService.UpdateManagerUserRoles(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }


        [HttpGet("Manager-User-Claims/{userId:int}")]
        public async Task<IActionResult> ManagerUserClaims([FromRoute] int userId)
        {
            var response = await _cusAuthorizationService.ManagerUserClaimsData(userId);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Edit-User-Claims")]
        public async Task<IActionResult> UpdateManagerUserClaims([FromBody] ManagerUserClaimsResponse request)
        {
            var response = await _cusAuthorizationService.UpdateMangerUserClaims(request);

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
