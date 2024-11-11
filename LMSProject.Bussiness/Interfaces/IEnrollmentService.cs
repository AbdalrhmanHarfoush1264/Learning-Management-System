using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.EnrollmentDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IEnrollmentService
    {
        public Task<CusResponse<string>> AddEnrollmentAsync(AddEnrollmentRequest request);
        public Task<CusResponse<string>> UpdateEnrollmentAsync(UpdateEnrollmentRequest request);
        public Task<CusResponse<string>> DeleteEnrollmentAsync(int Id);
        public Task<CusResponse<List<EnrollmentResponseDto>>> GetEnrollmentListAsync();
        public Task<PaginatedResult<EnrollmentResponseDto>> GetEnrollmentPaginatedListAsync(EnrollmentPaginatedListRequest request);
        public Task<CusResponse<EnrollmentResponseDto>> GetEnrollmentByIdAsync(int enrollmentId);
    }
}
