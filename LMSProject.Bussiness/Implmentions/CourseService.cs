using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.CourseDTOS;
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
    public class CourseService : ICourseService
    {
        #region Fileds
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public CourseService(IGenericRepository<Course> courseRepository, UserManager<User> userManager)
        {
            _courseRepository = courseRepository;
            _userManager = userManager;
        }
        #endregion

        #region Public-Methods
        public async Task<CusResponse<string>> AddCourseAsync(AddCourseRequest request)
        {
            try
            {
                var teacher = await _userManager.FindByIdAsync(request.TeacherId.ToString());
                if (teacher == null)
                {
                    return ErrorResponse($"Teacher with Id : {request.TeacherId} not found!", HttpStatusCode.NotFound);
                }

                var IsTeacher = await _userManager.IsInRoleAsync(teacher, "teacher");
                if (!IsTeacher)
                {
                    return ErrorResponse($"Teacher with Id : {request.TeacherId} not found!", HttpStatusCode.NotFound);
                }

                var course = new Course();
                course.Title = request.Title;
                course.Description = request.Description;
                course.Level = request.Level;
                course.CreatedDate = DateTime.UtcNow;
                course.UserId = request.TeacherId;

                var result = await _courseRepository.AddAsync(course);
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
        public async Task<CusResponse<string>> UpdateCourseAsync(UpdateCourseRequest request)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(request.CourseId);
                if (course == null)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                course.Title = request.Title;
                course.Description = request.Description;
                course.Level = request.Level;
                course.CreatedDate = DateTime.UtcNow;

                var result = await _courseRepository.UpdateAsync(course);
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
        public async Task<CusResponse<string>> DeleteCourseAsync(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    return ErrorResponse($"course with Id : {courseId} not found!", HttpStatusCode.NotFound);
                }

                var result = await _courseRepository.DeleteAsync(course);
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
        public async Task<CusResponse<List<CourseResponseDto>>> GetCoursesListAsync()
        {
            try
            {
                var coursesList = await _courseRepository.GetTableNoTracking().Include(x => x.User).ToListAsync();
                var coursesListDto = new List<CourseResponseDto>();

                foreach (var item in coursesList)
                {
                    var courseDto = new CourseResponseDto()
                    {
                        Id = item.CourseId,
                        Title = item.Title,
                        Description = item.Description ?? "no found!",
                        Level = item.Level ?? "no found!",
                        CreatedTime = new DateOnly(item.CreatedDate.Year, item.CreatedDate.Month, item.CreatedDate.Day),
                        TeacherName = item.User.FullName
                    };

                    coursesListDto.Add(courseDto);
                }

                return new CusResponse<List<CourseResponseDto>>
                {
                    IsSuccess = true,
                    Message = coursesListDto.Any() ? "Courses retrieved successfully." : "No courses available.",
                    Data = coursesListDto,
                    DataCount = coursesListDto.Count,
                    StatusCode = coursesListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<CourseResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<CourseResponseDto>> GetCoursesPaginatedListAsync(CoursePaginatedListRequest request)
        {
            var query = _courseRepository.GetTableNoTracking().Include(x => x.User)
                .Select(course => new CourseResponseDto
                {
                    Id = course.CourseId,
                    Title = course.Title,
                    Description = course.Description ?? "no found!",
                    Level = course.Level ?? "no found!",
                    CreatedTime = new DateOnly(course.CreatedDate.Year, course.CreatedDate.Month, course.CreatedDate.Day),
                    TeacherName = course.User.FullName
                });

            if (!query.Any())
            {
                return new PaginatedResult<CourseResponseDto>(new List<CourseResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        public async Task<CusResponse<CourseResponseDto>> GetCourseByIdAsync(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetTableNoTracking().Include(x => x.User)
                    .Where(x => x.CourseId == courseId).FirstOrDefaultAsync();

                if (course == null)
                {
                    return new CusResponse<CourseResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"Course with Id : {courseId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var courseDto = new CourseResponseDto()
                {
                    Id = course.CourseId,
                    Title = course.Title,
                    Description = course.Description ?? "no found!",
                    Level = course.Level ?? "no found!",
                    CreatedTime = new DateOnly(course.CreatedDate.Year, course.CreatedDate.Month, course.CreatedDate.Day),
                    TeacherName = course.User.FullName
                };

                return new CusResponse<CourseResponseDto>
                {
                    IsSuccess = true,
                    Message = "Courses retrieved successfully.",
                    Data = courseDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<CourseResponseDto>
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
