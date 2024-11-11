using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.GradeDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class GradeService : IGradeService
    {
        #region Fileds
        private readonly IGenericRepository<Grade> _gradeRepository;
        private readonly ISubmissionService _submissionService;
        #endregion

        #region Constructors
        public GradeService(IGenericRepository<Grade> gradeRepository, ISubmissionService submissionService)
        {
            _gradeRepository = gradeRepository;
            _submissionService = submissionService;
        }
        #endregion


        #region Public-Methods

        public async Task<CusResponse<string>> AddGradeAsync(AddGradeRequest request)
        {
            try
            {
                var submission = await _submissionService.GetSubmissionByIdAsync(request.SubmissionId);
                if (!submission.IsSuccess)
                {
                    return ErrorResponse($"submission with Id : {request.SubmissionId} not found!", HttpStatusCode.NotFound);
                }



                var grade = new Grade();
                grade.SubmissionId = request.SubmissionId;
                grade.grade = request.GradeValue;

                var result = await _gradeRepository.AddAsync(grade);

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
        public async Task<CusResponse<string>> UpdateGradeAsync(UpdateGradeRequest request)
        {
            try
            {
                var grade = await _gradeRepository.GetByIdAsync(request.Id);
                if (grade == null)
                {
                    return ErrorResponse($"grade with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }


                grade.SubmissionId = request.SubmissionId;
                grade.grade = request.GradeValue;

                var result = await _gradeRepository.UpdateAsync(grade);

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
        public async Task<CusResponse<string>> DeleteGradeAsync(int GradeId)
        {
            try
            {
                var grade = await _gradeRepository.GetByIdAsync(GradeId);
                if (grade == null)
                {
                    return ErrorResponse($"grade with Id : {GradeId} not found!", HttpStatusCode.NotFound);
                }

                var result = await _gradeRepository.DeleteAsync(grade);

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
        public async Task<CusResponse<List<GradeResponseDto>>> GetGradeListAsync()
        {
            try
            {
                var resultList = await _gradeRepository.GetTableNoTracking()
                    .Include(x => x.Submission).ToListAsync();

                var GradeListDto = new List<GradeResponseDto>();

                foreach (var item in resultList)
                {
                    var GradeDto = new GradeResponseDto
                    {
                        Id = item.GradeId,
                        Value = item.grade,
                        SubmissionId = item.Submission.SubmissionId,
                        SubmissionContent = item.Submission.Content
                    };

                    GradeListDto.Add(GradeDto);
                }

                return new CusResponse<List<GradeResponseDto>>
                {
                    IsSuccess = true,
                    Message = GradeListDto.Any() ? "Grade retrieved successfully." : "No grade available.",
                    Data = GradeListDto,
                    DataCount = GradeListDto.Count,
                    StatusCode = GradeListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<GradeResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<GradeResponseDto>> GetGradePaginatedListAsync(GradePaginatedListRequest request)
        {
            var query = _gradeRepository.GetTableNoTracking()
                .Include(x => x.Submission)
                .Select(grade => new GradeResponseDto
                {
                    Id = grade.GradeId,
                    Value = grade.grade,
                    SubmissionId = grade.Submission.SubmissionId,
                    SubmissionContent = grade.Submission.Content
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<GradeResponseDto>(new List<GradeResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        public async Task<CusResponse<GradeResponseDto>> GetGradeByIdAsync(int GradeId)
        {
            try
            {
                var grade = await _gradeRepository.GetTableNoTracking()
                    .Where(x => x.GradeId == GradeId)
                    .Include(x => x.Submission).FirstOrDefaultAsync();

                if (grade == null)
                {
                    return new CusResponse<GradeResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"Grade with Id :{GradeId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }


                var GradeDto = new GradeResponseDto
                {
                    Id = grade.GradeId,
                    Value = grade.grade,
                    SubmissionId = grade.Submission.SubmissionId,
                    SubmissionContent = grade.Submission.Content
                };

                return new CusResponse<GradeResponseDto>
                {
                    IsSuccess = true,
                    Message = "Grade retrieved successfully.",
                    Data = GradeDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<GradeResponseDto>
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
