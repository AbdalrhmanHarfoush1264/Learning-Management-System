using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.NotificationDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface INotificationServicecs
    {
        public Task<CusResponse<List<NoticationResponseDto>>> GetNotificationListAsync();
        public Task<CusResponse<NoticationResponseDto>> GetNotificationByIdAsync(int id);
        public Task<CusResponse<string>> AddNoticationAsync(AddNotificationRequest request);
        public Task<CusResponse<string>> UpdateNoticationAsync(UpdateNotificationRequest request);
        public Task<CusResponse<string>> DeleteNoticationAsync(int Id);
        public Task<PaginatedResult<NoticationResponseDto>> GetNotificationPaginatedListAsync(NotificationPaginatedListRequest request);
    }
}
