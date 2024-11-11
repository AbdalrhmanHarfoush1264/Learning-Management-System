using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.GradeDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IGradeService
    {
        public Task<CusResponse<string>> AddGradeAsync(AddGradeRequest request);
        public Task<CusResponse<string>> UpdateGradeAsync(UpdateGradeRequest request);
        public Task<CusResponse<string>> DeleteGradeAsync(int GradeId);
        public Task<CusResponse<List<GradeResponseDto>>> GetGradeListAsync();
        public Task<PaginatedResult<GradeResponseDto>> GetGradePaginatedListAsync(GradePaginatedListRequest request);
        public Task<CusResponse<GradeResponseDto>> GetGradeByIdAsync(int GradeId);
    }
}
