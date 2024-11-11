using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.CertificateDTOS;
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
    public class CertificateService : ICertificateService
    {
        #region Fileds
        private readonly IGenericRepository<Certificate> _certificateRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;
        //private readonly IGenericRepository<>
        #endregion

        #region Constructors
        public CertificateService(IGenericRepository<Certificate> certificateRepository,
            UserManager<User> userManager, ICourseService courseService)
        {
            _certificateRepository = certificateRepository;
            _userManager = userManager;
            _courseService = courseService;
        }
        #endregion


        #region Methods

        public async Task<CusResponse<string>> AddCertificateAsync(AddCertificateRequest request)
        {
            try
            {
                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                var certificate = new Certificate();
                certificate.IssueDate = DateTime.UtcNow;
                certificate.CourseId = request.CourseId;
                certificate.UserId = request.StudentId;

                var result = await _certificateRepository.AddAsync(certificate);

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
        public async Task<CusResponse<string>> UpdateCertificateAsync(UpdateCertificateRequest request)
        {
            try
            {

                var certificate = await _certificateRepository.GetByIdAsync(request.CertificateId);
                if (certificate == null)
                {
                    return ErrorResponse($"Certificate with Id : {request.CertificateId} not found!", HttpStatusCode.NotFound);
                }

                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                certificate.CourseId = request.CourseId;
                certificate.UserId = request.StudentId;
                certificate.IssueDate = DateTime.UtcNow;

                var result = await _certificateRepository.UpdateAsync(certificate);
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
        public async Task<CusResponse<string>> DeleteCertificateAsync(int certificateId)
        {
            try
            {
                var certificate = await _certificateRepository.GetByIdAsync(certificateId);
                if (certificate == null)
                {
                    return ErrorResponse($"Certificate with Id : {certificateId} not found!", HttpStatusCode.NotFound);
                }

                var result = await _certificateRepository.DeleteAsync(certificate);

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
        public async Task<CusResponse<List<CertificateResponseDto>>> GetCertificateListAsync()
        {
            try
            {
                var resultList = await _certificateRepository.GetTableNoTracking().Include(x => x.Course)
                    .Include(x => x.User).ToListAsync();

                var certificationResponseListDto = new List<CertificateResponseDto>();


                foreach (var certificate in resultList)
                {
                    var certificateDto = new CertificateResponseDto
                    {
                        Id = certificate.CertificateId,
                        IssueDate = new DateOnly(certificate.IssueDate.Year, certificate.IssueDate.Month, certificate.IssueDate.Day),
                        StudentName = certificate.User.FullName,
                        CourseName = certificate.Course.Title
                    };

                    certificationResponseListDto.Add(certificateDto);
                }

                return new CusResponse<List<CertificateResponseDto>>
                {
                    IsSuccess = true,
                    Message = certificationResponseListDto.Any() ? "Certification retrieved successfully." : "No certification available.",
                    Data = certificationResponseListDto,
                    DataCount = certificationResponseListDto.Count,
                    StatusCode = certificationResponseListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent
                };

            }
            catch (Exception ex)
            {
                return new CusResponse<List<CertificateResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<CertificateResponseDto>> GetCertificationPaginatedListAsync(CertificationPaginatedListRequest request)
        {
            var query = _certificateRepository.GetTableNoTracking().Include(x => x.User).Include(x => x.Course)
                .Select(Certificate => new CertificateResponseDto
                {
                    Id = Certificate.CertificateId,
                    IssueDate = new DateOnly(Certificate.IssueDate.Year, Certificate.IssueDate.Month, Certificate.IssueDate.Day),
                    CourseName = Certificate.Course.Title,
                    StudentName = Certificate.User.FullName
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<CertificateResponseDto>(new List<CertificateResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        public async Task<CusResponse<CertificateResponseDto>> GetCertificateByIdAsync(int certificateId)
        {
            try
            {
                var certificate = await _certificateRepository.GetTableNoTracking().Include(x => x.User)
                    .Include(x => x.Course).FirstOrDefaultAsync();

                if (certificate == null)
                {
                    return new CusResponse<CertificateResponseDto>()
                    {
                        IsSuccess = false,
                        Message = $"Certificate with Id : {certificateId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }


                var certificateDto = new CertificateResponseDto()
                {
                    Id = certificate.CertificateId,
                    IssueDate = new DateOnly(certificate.IssueDate.Year, certificate.IssueDate.Month, certificate.IssueDate.Day),
                    StudentName = certificate.User.FullName,
                    CourseName = certificate.Course.Title
                };

                return new CusResponse<CertificateResponseDto>()
                {
                    IsSuccess = true,
                    Message = "Certification retrieved successfully.",
                    Data = certificateDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new CusResponse<CertificateResponseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
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
