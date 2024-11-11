using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.AssignmentDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IAssignmentService
    {
        public Task<CusResponse<string>> AddAssignmentAsync(AddAssignmentRequest request);
        public Task<CusResponse<string>> UpdateAssignmentAsync(UpdateAssignmentRequest request);
        public Task<CusResponse<string>> DeleteAssignmentAsync(int Id);
        public Task<CusResponse<List<AssignmentResponseDto>>> GetAssignmentListAsync();
        public Task<CusResponse<AssignmentResponseDto>> GetAssignmentByIdAsync(int Id);
        public Task<PaginatedResult<AssignmentResponseDto>> GetAssignmentPaginatedListAsync(AssignmentPaginatedListRequest request);
    }
}
