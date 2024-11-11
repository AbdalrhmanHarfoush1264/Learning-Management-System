using LMSProject.Bussiness.Dtos.NotificationDTOS;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LMSProject.Prestention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        #region Fileds
        private readonly INotificationServicecs _notificationServicecs;
        #endregion

        #region Constructors
        public NotificationController(INotificationServicecs notificationServicecs)
        {
            _notificationServicecs = notificationServicecs;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetNotificationList()
        {
            var response = await _notificationServicecs.GetNotificationListAsync();

            if (!response.IsSuccess)
                return StatusCode(500, response);

            if (response.DataCount == 0)
                return StatusCode(204, response);

            return Ok(response);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetNotificationsPaginated([FromQuery] NotificationPaginatedListRequest request)
        {
            var response = await _notificationServicecs.GetNotificationPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetNotificationById([FromRoute] int Id)
        {
            var response = await _notificationServicecs.GetNotificationByIdAsync(Id);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> AddNotification([FromBody] AddNotificationRequest request)
        {

            var response = await _notificationServicecs.AddNoticationAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateNotificationById([FromBody] UpdateNotificationRequest request)
        {


            var response = await _notificationServicecs.UpdateNoticationAsync(request);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);


            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteNotificationById([FromRoute] int Id)
        {
            var response = await _notificationServicecs.DeleteNoticationAsync(Id);

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
