using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.SubmissionDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ISubmissionService
    {
        public Task<CusResponse<string>> AddSubmissionAsync(AddSubmissionRequest request);
        public Task<CusResponse<string>> UpdateSubmissionAsync(UpdateSubmissionRequest request);
        public Task<CusResponse<string>> DeleteSubmissionAsync(int submissionId);
        public Task<CusResponse<List<SubmissionResponseDto>>> GetSubmissionListAsync();
        public Task<PaginatedResult<SubmissionResponseDto>> GetSubmissionPaginatedListAsync(SubmissionPaginatedListRequest request);
        public Task<CusResponse<SubmissionResponseDto>> GetSubmissionByIdAsync(int submissionId);
    }
}
