using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.UserDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IUserService
    {
        public Task<CusResponse<string>> AddUserAsync(AddUserRequest request, string roleName);
        public Task<CusResponse<string>> UpdateUserAsync(UpdateUserRequest request);
        public Task<CusResponse<string>> DeleteUserAsync(int Id);
        public Task<CusResponse<List<UserResponseDto>>> GetUserListAsync();
        public Task<CusResponse<UserResponseDto>> GetUserByIdAsync(int Id);
        public Task<PaginatedResult<UserPaginatedListResponseDto>> GetUserPaginatedListAsync(UserPaginatedListRequest request);
        public Task<CusResponse<string>> ChangePasswordAsync(ChangeUserPasswordRequest request);
    }
}
