using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.CertificateDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ICertificateService
    {
        public Task<CusResponse<string>> AddCertificateAsync(AddCertificateRequest request);
        public Task<CusResponse<string>> UpdateCertificateAsync(UpdateCertificateRequest request);
        public Task<CusResponse<string>> DeleteCertificateAsync(int certificateId);
        public Task<CusResponse<List<CertificateResponseDto>>> GetCertificateListAsync();
        public Task<PaginatedResult<CertificateResponseDto>> GetCertificationPaginatedListAsync(CertificationPaginatedListRequest request);
        public Task<CusResponse<CertificateResponseDto>> GetCertificateByIdAsync(int certificateId);
    }
}
