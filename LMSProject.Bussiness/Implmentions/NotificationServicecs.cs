using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.NotificationDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using LMSProject.Data.Data.Enities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class NotificationServicecs : INotificationServicecs
    {
        #region Fileds
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructors
        public NotificationServicecs(IGenericRepository<Notification> notificationRepository,
           UserManager<User> userManager)
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }
        #endregion

        #region Public-Methods
        public async Task<CusResponse<string>> AddNoticationAsync(AddNotificationRequest request)
        {
            try
            {
                var student = await _userManager.FindByIdAsync(request.studentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"student with Id : {request.studentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"stuent with Id : {request.studentId} not found!", HttpStatusCode.NotFound);
                }

                var notication = new Notification();
                notication.Message = request.Message;
                notication.SendDate = DateTime.Now;
                notication.UserId = student.Id;

                var result = await _notificationRepository.AddAsync(notication);

                return new CusResponse<string>
                {
                    IsSuccess = result,
                    Message = result ? "Added Operation Successfully." : "Added Operation Failed.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest
                };

            }
            catch (Exception ex)
            {
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<string>> UpdateNoticationAsync(UpdateNotificationRequest request)
        {

            try
            {
                var notification = await _notificationRepository.GetByIdAsync(request.Id);
                if (notification == null)
                {
                    return ErrorResponse($"Notification with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                notification.Message = request.Message;
                notification.SendDate = DateTime.Now;

                var result = await _notificationRepository.UpdateAsync(notification);

                return new CusResponse<string>
                {
                    IsSuccess = result,
                    Message = result ? "Updated Operation Successfully." : "Updated Operation Failed.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {

                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<string>> DeleteNoticationAsync(int id)
        {

            try
            {
                var notification = await _notificationRepository.GetByIdAsync(id);
                if (notification == null)
                {
                    return ErrorResponse($"Notification with Id : {id} not found!", HttpStatusCode.NotFound);
                }

                var result = await _notificationRepository.DeleteAsync(notification);

                return new CusResponse<string>
                {
                    IsSuccess = result,
                    Message = result ? "Deleted Operation Successfully." : "Deleted Operation Failed.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<List<NoticationResponseDto>>> GetNotificationListAsync()
        {
            try
            {
                var resultList = await _notificationRepository.GetTableNoTracking().Include(x => x.User).ToListAsync();

                var noticationListDto = new List<NoticationResponseDto>();

                foreach (var item in resultList)
                {
                    var notificationDto = new NoticationResponseDto
                    {
                        Id = item.NotificationId,
                        message = item.Message,
                        date = new DateOnly(item.SendDate.Year, item.SendDate.Month, item.SendDate.Day),
                        StudentName = item.User.FullName
                    };

                    noticationListDto.Add(notificationDto);
                }

                return new CusResponse<List<NoticationResponseDto>>
                {
                    IsSuccess = true,
                    Message = noticationListDto.Any() ? "Notifications retrieved successfully." : "No notifications available.",
                    Data = noticationListDto,
                    DataCount = noticationListDto.Count,
                    StatusCode = noticationListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<NoticationResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<CusResponse<NoticationResponseDto>> GetNotificationByIdAsync(int id)
        {
            try
            {

                var notification = await _notificationRepository.GetTableNoTracking()
                    .Where(x => x.NotificationId == id).Include(x => x.User).FirstOrDefaultAsync();

                if (notification == null)
                {
                    return new CusResponse<NoticationResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"Notification with Id : {id} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var notificationDto = new NoticationResponseDto
                {
                    Id = notification.NotificationId,
                    message = notification.Message,
                    date = new DateOnly(notification.SendDate.Year, notification.SendDate.Month, notification.SendDate.Day),
                    StudentName = notification.User.FullName

                };


                return new CusResponse<NoticationResponseDto>
                {
                    IsSuccess = true,
                    Message = "Notifications retrieved successfully.",
                    Data = notificationDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<NoticationResponseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };


            }
        }

        public async Task<PaginatedResult<NoticationResponseDto>> GetNotificationPaginatedListAsync(NotificationPaginatedListRequest request)
        {
            var query = _notificationRepository.GetTableNoTracking().Include(x => x.User)
                .Select(notification => new NoticationResponseDto
                {
                    Id = notification.NotificationId,
                    message = notification.Message,
                    date = new DateOnly(notification.SendDate.Year, notification.SendDate.Month, notification.SendDate.Day),
                    StudentName = notification.User.FullName
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<NoticationResponseDto>(new List<NoticationResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        #endregion

        #region Private-Methods
        private CusResponse<string> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new CusResponse<string>
            {
                IsSuccess = false,
                Message = message,
                Data = null,
                DataCount = 0,
                StatusCode = statusCode
            };
        }
        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An error occurred: " + string.Join(", ", errors.Select(e => e.Description));
        }
        #endregion







    }
}
