using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ForumDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class ForumService : IForumService
    {
        #region Fileds
        private readonly IGenericRepository<Forum> _forumRepository;
        private readonly ICourseService _courseService;
        #endregion

        #region Constructors
        public ForumService(IGenericRepository<Forum> forumRepository, ICourseService courseService)
        {
            _forumRepository = forumRepository;
            _courseService = courseService;
        }
        #endregion

        #region Public-Methods

        public async Task<CusResponse<string>> AddForumAsync(AddForumRequest request)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                var forum = new Forum();
                forum.Title = request.Title;
                forum.CourseId = request.CourseId;

                var result = await _forumRepository.AddAsync(forum);

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

        public async Task<CusResponse<string>> UpdateForumAsync(UpdateForumRequest request)
        {
            try
            {
                var forum = await _forumRepository.GetByIdAsync(request.Id);
                if (forum == null)
                {
                    return ErrorResponse($"fourm with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                forum.Title = request.Title;
                forum.CourseId = request.CourseId;

                var result = await _forumRepository.UpdateAsync(forum);

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

        public async Task<CusResponse<string>> DeleteForumAsync(int Id)
        {
            try
            {
                var forum = await _forumRepository.GetByIdAsync(Id);
                if (forum == null)
                {
                    return ErrorResponse($"fourm with Id : {Id} not found!", HttpStatusCode.NotFound);
                }

                var result = await _forumRepository.DeleteAsync(forum);

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

        public async Task<CusResponse<List<ForumResponseDto>>> GetForumListAsync()
        {
            try
            {
                var resultList = await _forumRepository.GetTableNoTracking().Include(x => x.Course).ToListAsync();

                var ForumListDto = new List<ForumResponseDto>();

                foreach (var item in resultList)
                {
                    var forumDto = new ForumResponseDto
                    {
                        Id = item.ForumId,
                        Title = item.Title,
                        CourseName = item.Course.Title
                    };

                    ForumListDto.Add(forumDto);
                }

                return new CusResponse<List<ForumResponseDto>>
                {
                    IsSuccess = true,
                    Message = ForumListDto.Any() ? "Forums retrieved successfully." : "No Forums available.",
                    Data = ForumListDto,
                    DataCount = ForumListDto.Count,
                    StatusCode = ForumListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<ForumResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<PaginatedResult<ForumResponseDto>> GetForumsPaginatedListAsync(ForumPaginatedListRequest request)
        {
            var query = _forumRepository.GetTableNoTracking().Include(x => x.Course)
                .Select(Forum => new ForumResponseDto
                {
                    Id = Forum.ForumId,
                    Title = Forum.Title,
                    CourseName = Forum.Course.Title
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<ForumResponseDto>(new List<ForumResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }

        public async Task<CusResponse<ForumResponseDto>> GetForumByIdAsync(int Id)
        {
            try
            {
                var forum = await _forumRepository.GetTableNoTracking()
                    .Where(x => x.ForumId == Id).Include(x => x.Course).FirstOrDefaultAsync();

                if (forum == null)
                {
                    return new CusResponse<ForumResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"fourm with Id:{Id} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }


                var forumDto = new ForumResponseDto
                {
                    Id = forum.ForumId,
                    Title = forum.Title,
                    CourseName = forum.Course.Title
                };


                return new CusResponse<ForumResponseDto>
                {
                    IsSuccess = true,
                    Message = "Forums retrieved successfully.",
                    Data = forumDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<ForumResponseDto>
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
