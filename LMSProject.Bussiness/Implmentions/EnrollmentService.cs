using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.EnrollmentDTOS;
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
    public class EnrollmentService : IEnrollmentService
    {
        #region Fileds
        private readonly IGenericRepository<Enrollment> _enrollmentRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;
        #endregion


        #region Constructors
        public EnrollmentService(IGenericRepository<Enrollment> enrollmentRepository,
            UserManager<User> userManager, ICourseService courseService)
        {
            _enrollmentRepository = enrollmentRepository;
            _userManager = userManager;
            _courseService = courseService;
        }
        #endregion

        #region Public-Methods

        public async Task<CusResponse<string>> AddEnrollmentAsync(AddEnrollmentRequest request)
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

                var enrollment = new Enrollment();
                enrollment.CourserId = request.CourseId;
                enrollment.UserId = request.StudentId;
                enrollment.EnrollmentDate = DateTime.UtcNow;

                var result = await _enrollmentRepository.AddAsync(enrollment);

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
        public async Task<CusResponse<string>> UpdateEnrollmentAsync(UpdateEnrollmentRequest request)
        {

            try
            {

                var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id);
                if (enrollment == null)
                {
                    return ErrorResponse($"enrollment with Id : {request.Id} not found!", HttpStatusCode.NotFound);
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
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                enrollment.CourserId = request.CourseId;
                enrollment.UserId = request.StudentId;
                enrollment.EnrollmentDate = DateTime.UtcNow;

                var result = await _enrollmentRepository.UpdateAsync(enrollment);

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
        public async Task<CusResponse<string>> DeleteEnrollmentAsync(int Id)
        {

            try
            {

                var enrollment = await _enrollmentRepository.GetByIdAsync(Id);
                if (enrollment == null)
                {
                    return ErrorResponse($"enrollment with Id : {Id} not found!", HttpStatusCode.NotFound);
                }


                var result = await _enrollmentRepository.DeleteAsync(enrollment);
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
        public async Task<CusResponse<List<EnrollmentResponseDto>>> GetEnrollmentListAsync()
        {
            try
            {
                var resultList = await _enrollmentRepository.GetTableNoTracking()
                    .Include(x => x.User).Include(x => x.Course).ToListAsync();

                var enrollmentListDto = new List<EnrollmentResponseDto>();

                foreach (var item in resultList)
                {
                    var enrollmentDto = new EnrollmentResponseDto
                    {
                        Id = item.EnrollmentId,
                        CourseName = item.Course.Title,
                        StudentName = item.User.FullName,
                        EnrollmentDate = new DateOnly(item.EnrollmentDate.Year, item.EnrollmentDate.Month, item.EnrollmentDate.Day)
                    };

                    enrollmentListDto.Add(enrollmentDto);
                }

                return new CusResponse<List<EnrollmentResponseDto>>
                {
                    IsSuccess = true,
                    Message = enrollmentListDto.Any() ? "Enrollments retrieved successfully." : "No enrollments available.",
                    Data = enrollmentListDto,
                    DataCount = enrollmentListDto.Count,
                    StatusCode = enrollmentListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<EnrollmentResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<EnrollmentResponseDto>> GetEnrollmentPaginatedListAsync(EnrollmentPaginatedListRequest request)
        {
            var query = _enrollmentRepository.GetTableNoTracking()
                .Include(x => x.User).Include(x => x.Course)
                .Select(enrollment => new EnrollmentResponseDto
                {
                    Id = enrollment.EnrollmentId,
                    CourseName = enrollment.Course.Title,
                    StudentName = enrollment.User.FullName,
                    EnrollmentDate = new DateOnly(enrollment.EnrollmentDate.Year, enrollment.EnrollmentDate.Month, enrollment.EnrollmentDate.Day)
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<EnrollmentResponseDto>(new List<EnrollmentResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        public async Task<CusResponse<EnrollmentResponseDto>> GetEnrollmentByIdAsync(int enrollmentId)
        {
            try
            {
                var enrollment = await _enrollmentRepository.GetTableNoTracking()
                    .Where(x => x.EnrollmentId == enrollmentId)
                    .Include(x => x.User).Include(x => x.Course).FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    return new CusResponse<EnrollmentResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"enrollment with Id : {enrollmentId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                var enrollmentDto = new EnrollmentResponseDto
                {
                    Id = enrollment.EnrollmentId,
                    CourseName = enrollment.Course.Title,
                    StudentName = enrollment.User.FullName,
                    EnrollmentDate = new DateOnly(enrollment.EnrollmentDate.Year, enrollment.EnrollmentDate.Month, enrollment.EnrollmentDate.Day)
                };

                return new CusResponse<EnrollmentResponseDto>
                {
                    IsSuccess = true,
                    Message = "Enrollments retrieved successfully.",
                    Data = enrollmentDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<EnrollmentResponseDto>
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
