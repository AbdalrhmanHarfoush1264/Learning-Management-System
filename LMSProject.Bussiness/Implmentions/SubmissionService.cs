using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.SubmissionDTOS;
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
    public class SubmissionService : ISubmissionService
    {
        #region Fileds
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<Submission> _submissionRepository;
        private readonly IAssignmentService _assignmentService;
        #endregion

        #region Constructors
        public SubmissionService(UserManager<User> userManager, IGenericRepository<Submission> submissionRepository,
            IAssignmentService assignmentService)
        {
            _userManager = userManager;
            _submissionRepository = submissionRepository;
            _assignmentService = assignmentService;
        }
        #endregion

        #region Public-Methods

        public async Task<CusResponse<string>> AddSubmissionAsync(AddSubmissionRequest request)
        {
            try
            {
                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"student with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"student with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var assignment = await _assignmentService.GetAssignmentByIdAsync(request.AssignmentId);
                if (!assignment.IsSuccess)
                {
                    return ErrorResponse($"assignment with Id : {request.AssignmentId} not found!", HttpStatusCode.NotFound);
                }

                var submission = new Submission();
                submission.Content = request.Content;
                submission.AssignmentId = request.AssignmentId;
                submission.UserId = request.StudentId;
                submission.SubmissionDate = DateTime.UtcNow;

                var result = await _submissionRepository.AddAsync(submission);

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

        public async Task<CusResponse<string>> UpdateSubmissionAsync(UpdateSubmissionRequest request)
        {
            try
            {
                var submission = await _submissionRepository.GetByIdAsync(request.Id);
                if (submission == null)
                {
                    return ErrorResponse($"submission with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"student with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"student with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var assignment = await _assignmentService.GetAssignmentByIdAsync(request.AssignmentId);
                if (!assignment.IsSuccess)
                {
                    return ErrorResponse($"assignment with Id : {request.AssignmentId} not found!", HttpStatusCode.NotFound);
                }

                submission.Content = request.Content;
                submission.AssignmentId = request.AssignmentId;
                submission.UserId = request.StudentId;
                submission.SubmissionDate = DateTime.UtcNow;

                var result = await _submissionRepository.UpdateAsync(submission);

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

        public async Task<CusResponse<string>> DeleteSubmissionAsync(int submissionId)
        {
            try
            {
                var submission = await _submissionRepository.GetByIdAsync(submissionId);
                if (submission == null)
                {
                    return ErrorResponse($"submission with Id : {submissionId} not found!", HttpStatusCode.NotFound);
                }

                var result = await _submissionRepository.DeleteAsync(submission);

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

        public async Task<CusResponse<List<SubmissionResponseDto>>> GetSubmissionListAsync()
        {
            try
            {
                var resultList = await _submissionRepository.GetTableNoTracking()
                    .Include(x => x.Assignment).Include(x => x.User).ToListAsync();

                var SubmissionListDto = new List<SubmissionResponseDto>();

                foreach (var item in resultList)
                {
                    var SubmissionDto = new SubmissionResponseDto
                    {
                        Id = item.SubmissionId,
                        Content = item.Content,
                        SubmissionDate = new DateOnly(item.SubmissionDate.Year, item.SubmissionDate.Month, item.SubmissionDate.Day),
                        AssignmentName = item.Assignment.Title,
                        StudentName = item.User.FullName
                    };

                    SubmissionListDto.Add(SubmissionDto);
                }

                return new CusResponse<List<SubmissionResponseDto>>
                {
                    IsSuccess = true,
                    Message = SubmissionListDto.Any() ? "Submissions retrieved successfully." : "No submissions available.",
                    Data = SubmissionListDto,
                    DataCount = SubmissionListDto.Count,
                    StatusCode = SubmissionListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<SubmissionResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<PaginatedResult<SubmissionResponseDto>> GetSubmissionPaginatedListAsync(SubmissionPaginatedListRequest request)
        {
            var query = _submissionRepository.GetTableNoTracking()
                .Include(x => x.User).Include(x => x.Assignment)
                .Select(submission => new SubmissionResponseDto
                {
                    Id = submission.SubmissionId,
                    Content = submission.Content,
                    SubmissionDate = new DateOnly(submission.SubmissionDate.Year, submission.SubmissionDate.Month, submission.SubmissionDate.Day),
                    AssignmentName = submission.Assignment.Title,
                    StudentName = submission.User.FullName
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<SubmissionResponseDto>(new List<SubmissionResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }

        public async Task<CusResponse<SubmissionResponseDto>> GetSubmissionByIdAsync(int submissionId)
        {
            try
            {
                var submission = await _submissionRepository.GetTableNoTracking()
                    .Where(x => x.SubmissionId == submissionId)
                    .Include(x => x.Assignment).Include(x => x.User).FirstOrDefaultAsync();


                if (submission == null)
                {
                    return new CusResponse<SubmissionResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"Submission with Id : {submissionId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var SubmissionDto = new SubmissionResponseDto
                {
                    Id = submission.SubmissionId,
                    Content = submission.Content,
                    SubmissionDate = new DateOnly(submission.SubmissionDate.Year, submission.SubmissionDate.Month, submission.SubmissionDate.Day),
                    AssignmentName = submission.Assignment.Title,
                    StudentName = submission.User.FullName
                };

                return new CusResponse<SubmissionResponseDto>
                {
                    IsSuccess = true,
                    Message = "Submissions retrieved successfully.",
                    Data = SubmissionDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<SubmissionResponseDto>
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
