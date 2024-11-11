using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ForumDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IForumService
    {
        public Task<CusResponse<string>> AddForumAsync(AddForumRequest request);
        public Task<CusResponse<string>> UpdateForumAsync(UpdateForumRequest request);
        public Task<CusResponse<string>> DeleteForumAsync(int Id);
        public Task<CusResponse<List<ForumResponseDto>>> GetForumListAsync();
        public Task<PaginatedResult<ForumResponseDto>> GetForumsPaginatedListAsync(ForumPaginatedListRequest request);
        public Task<CusResponse<ForumResponseDto>> GetForumByIdAsync(int Id);
    }
}
