using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ForumPostDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IForumPostService
    {
        public Task<CusResponse<string>> AddForumPostAsync(AddForumPostRequest request);
        public Task<CusResponse<string>> UpdateForumPostAsync(UpdateForumPostRequest request);
        public Task<CusResponse<string>> DeleteForumPostAsync(int Id);
        public Task<CusResponse<List<ForumPostResponseDto>>> GetForumPostListAsync();
        public Task<PaginatedResult<ForumPostResponseDto>> GetForumPostPaginatedListAsync(ForumPostPaginatedListRequest request);
        public Task<CusResponse<ForumPostResponseDto>> GetForumPostByIdAsync(int Id);
    }
}
