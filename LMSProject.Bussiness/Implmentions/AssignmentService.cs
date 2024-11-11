using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.AssignmentDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class AssignmentService : IAssignmentService
    {
        #region Fileds
        private readonly IGenericRepository<Assignment> _assignmentRepository;
        private readonly ICourseService _courseService;
        #endregion

        #region Constructors
        public AssignmentService(IGenericRepository<Assignment> assignmentRepository, ICourseService courseService)
        {
            _assignmentRepository = assignmentRepository;
            _courseService = courseService;
        }
        #endregion

        #region Public-Methods
        public async Task<CusResponse<string>> AddAssignmentAsync(AddAssignmentRequest request)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                var assignment = new Assignment();
                assignment.CourseId = request.CourseId;
                assignment.Title = request.Title;
                assignment.Description = request.Description;
                assignment.CreatedDate = DateTime.UtcNow;

                var result = await _assignmentRepository.AddAsync(assignment);

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

        public async Task<CusResponse<string>> UpdateAssignmentAsync(UpdateAssignmentRequest request)
        {
            try
            {

                var assignment = await _assignmentRepository.GetByIdAsync(request.Id);
                if (assignment == null)
                {
                    return ErrorResponse($"assignment with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }


                assignment.CourseId = request.CourseId;
                assignment.Title = request.Title;
                assignment.Description = request.Description;
                assignment.CreatedDate = DateTime.UtcNow;

                var result = await _assignmentRepository.UpdateAsync(assignment);

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

        public async Task<CusResponse<string>> DeleteAssignmentAsync(int Id)
        {
            try
            {

                var assignment = await _assignmentRepository.GetByIdAsync(Id);
                if (assignment == null)
                {
                    return ErrorResponse($"assignment with Id : {Id} not found!", HttpStatusCode.NotFound);
                }

                var result = await _assignmentRepository.DeleteAsync(assignment);

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

        public async Task<CusResponse<List<AssignmentResponseDto>>> GetAssignmentListAsync()
        {
            try
            {
                var resultList = await _assignmentRepository.GetTableNoTracking()
                    .Include(x => x.Course).ToListAsync();

                var assignmentListDto = new List<AssignmentResponseDto>();

                foreach (var item in resultList)
                {
                    var assignmentDto = new AssignmentResponseDto
                    {
                        Id = item.AssignmentId,
                        Title = item.Title,
                        Description = item.Description ?? "no found!",
                        CreatedDate = new DateOnly(item.CreatedDate.Year, item.CreatedDate.Month, item.CreatedDate.Day),
                        CourseName = item.Course.Title

                    };

                    assignmentListDto.Add(assignmentDto);
                }

                return new CusResponse<List<AssignmentResponseDto>>
                {
                    IsSuccess = true,
                    Message = assignmentListDto.Any() ? "Assignmentions retrieved successfully." : "No assignmentions available.",
                    Data = assignmentListDto,
                    DataCount = assignmentListDto.Count,
                    StatusCode = assignmentListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<AssignmentResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<CusResponse<AssignmentResponseDto>> GetAssignmentByIdAsync(int Id)
        {
            try
            {
                var assignment = await _assignmentRepository.GetTableNoTracking()
                    .Where(x => x.AssignmentId == Id)
                    .Include(x => x.Course).FirstOrDefaultAsync();

                if (assignment == null)
                {
                    return new CusResponse<AssignmentResponseDto>
                    {


                        IsSuccess = false,
                        Message = $"assignment with Id : {Id} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var assignmentDto = new AssignmentResponseDto
                {
                    Id = assignment.AssignmentId,
                    Title = assignment.Title,
                    Description = assignment.Description ?? "no found!",
                    CreatedDate = new DateOnly(assignment.CreatedDate.Year, assignment.CreatedDate.Month, assignment.CreatedDate.Day),
                    CourseName = assignment.Course.Title
                };


                return new CusResponse<AssignmentResponseDto>
                {
                    IsSuccess = true,
                    Message = "Assignmentions retrieved successfully.",
                    Data = assignmentDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<AssignmentResponseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<PaginatedResult<AssignmentResponseDto>> GetAssignmentPaginatedListAsync(AssignmentPaginatedListRequest request)
        {
            var query = _assignmentRepository.GetTableNoTracking().Include(x => x.Course)
                .Select(assignment => new AssignmentResponseDto
                {
                    Id = assignment.AssignmentId,
                    Title = assignment.Title,
                    Description = assignment.Description ?? "no found.",
                    CreatedDate = new DateOnly(assignment.CreatedDate.Year, assignment.CreatedDate.Month, assignment.CreatedDate.Day),
                    CourseName = assignment.Course.Title
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<AssignmentResponseDto>(new List<AssignmentResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
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
