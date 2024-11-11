using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ForumPostDTOS;
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
    public class ForumPostService : IForumPostService
    {
        #region Fileds
        private readonly IGenericRepository<ForumPost> _forumPostRepository;
        private readonly IForumService _forumService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public ForumPostService(IGenericRepository<ForumPost> forumPostRepository, IForumService forumService, UserManager<User> userManager)
        {
            _forumPostRepository = forumPostRepository;
            _forumService = forumService;
            _userManager = userManager;
        }
        #endregion


        #region Public-Methods
        public async Task<CusResponse<string>> AddForumPostAsync(AddForumPostRequest request)
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
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var forum = await _forumService.GetForumByIdAsync(request.ForumId);
                if (!forum.IsSuccess)
                {
                    return ErrorResponse($"forum with Id : {request.ForumId} not found!", HttpStatusCode.NotFound);
                }

                var forumPost = new ForumPost();
                forumPost.Content = request.Content;
                forumPost.PostDate = DateTime.UtcNow;
                forumPost.ForumId = request.ForumId;
                forumPost.UserId = request.StudentId;

                var result = await _forumPostRepository.AddAsync(forumPost);

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

        public async Task<CusResponse<string>> UpdateForumPostAsync(UpdateForumPostRequest request)
        {
            try
            {
                var forumPost = await _forumPostRepository.GetByIdAsync(request.Id);
                if (forumPost == null)
                {
                    return ErrorResponse($"forumPost with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return ErrorResponse($"student with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return ErrorResponse($"stuent with Id : {request.StudentId} not found!", HttpStatusCode.NotFound);
                }

                var forum = await _forumService.GetForumByIdAsync(request.ForumId);
                if (!forum.IsSuccess)
                {
                    return ErrorResponse($"forum with Id : {request.ForumId} not found!", HttpStatusCode.NotFound);
                }

                forumPost.Content = request.Content;
                forumPost.PostDate = DateTime.UtcNow;
                forumPost.ForumId = request.ForumId;
                forumPost.UserId = request.StudentId;

                var result = await _forumPostRepository.UpdateAsync(forumPost);

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

        public async Task<CusResponse<string>> DeleteForumPostAsync(int Id)
        {
            try
            {
                var forumPost = await _forumPostRepository.GetByIdAsync(Id);
                if (forumPost == null)
                {
                    return ErrorResponse($"forumPost with Id : {Id} not found!", HttpStatusCode.NotFound);
                }


                var result = await _forumPostRepository.DeleteAsync(forumPost);

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

        public async Task<CusResponse<List<ForumPostResponseDto>>> GetForumPostListAsync()
        {
            try
            {
                var resultList = await _forumPostRepository.GetTableNoTracking()
                    .Include(x => x.Forum).Include(x => x.User).ToListAsync();

                var forumPostListDto = new List<ForumPostResponseDto>();

                foreach (var item in resultList)
                {
                    var forumPostDto = new ForumPostResponseDto
                    {
                        Id = item.ForumPostId,
                        Content = item.Content,
                        PostDate = new DateOnly(item.PostDate.Year, item.PostDate.Month, item.PostDate.Day),
                        ForumTitle = item.Forum.Title,
                        StudentName = item.User.FullName
                    };

                    forumPostListDto.Add(forumPostDto);
                }

                return new CusResponse<List<ForumPostResponseDto>>
                {
                    IsSuccess = true,
                    Message = forumPostListDto.Any() ? "ForumPost retrieved successfully." : "No forumPost available.",
                    Data = forumPostListDto,
                    DataCount = forumPostListDto.Count,
                    StatusCode = forumPostListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<ForumPostResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<PaginatedResult<ForumPostResponseDto>> GetForumPostPaginatedListAsync(ForumPostPaginatedListRequest request)
        {
            var query = _forumPostRepository.GetTableNoTracking()
                .Include(x => x.User).Include(x => x.Forum)
                .Select(forumPost => new ForumPostResponseDto
                {
                    Id = forumPost.ForumPostId,
                    Content = forumPost.Content,
                    PostDate = new DateOnly(forumPost.PostDate.Year, forumPost.PostDate.Month, forumPost.PostDate.Day),
                    ForumTitle = forumPost.Forum.Title,
                    StudentName = forumPost.User.FullName
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<ForumPostResponseDto>(new List<ForumPostResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }

        public async Task<CusResponse<ForumPostResponseDto>> GetForumPostByIdAsync(int Id)
        {
            try
            {
                var forumPost = await _forumPostRepository.GetTableNoTracking()
                    .Where(x => x.ForumPostId == Id)
                    .Include(x => x.Forum).Include(x => x.User).FirstOrDefaultAsync();


                if (forumPost == null)
                {
                    return new CusResponse<ForumPostResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"ForumPost with Id {Id} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var forumPostDto = new ForumPostResponseDto
                {
                    Id = forumPost.ForumPostId,
                    Content = forumPost.Content,
                    PostDate = new DateOnly(forumPost.PostDate.Year, forumPost.PostDate.Month, forumPost.PostDate.Day),
                    ForumTitle = forumPost.Forum.Title,
                    StudentName = forumPost.User.FullName
                };


                return new CusResponse<ForumPostResponseDto>
                {
                    IsSuccess = true,
                    Message = "ForumPost retrieved successfully.",
                    Data = forumPostDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<ForumPostResponseDto>
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
