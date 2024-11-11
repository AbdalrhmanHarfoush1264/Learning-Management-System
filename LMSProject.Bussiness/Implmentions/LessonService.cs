using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.LessonDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class LessonService : ILessonService
    {
        #region Fileds
        private readonly IGenericRepository<Lesson> _lessonRepository;
        private readonly IVideoService _videoService;
        #endregion

        #region Constructors
        public LessonService(IGenericRepository<Lesson> lessonRepository, IVideoService videoService)
        {
            _lessonRepository = lessonRepository;
            _videoService = videoService;
        }
        #endregion


        #region Handle_Functons
        public async Task<CusResponse<string>> AddLessonAsync(AddLessonRequest request)
        {
            try
            {
                string? videoUrl = await _videoService.UploadVideoAsync(request.videoFile);

                var lesson = new Lesson
                {
                    Title = request.Title,
                    Content = request.Content,
                    VideoUrl = videoUrl,
                    ModuleId = request.ModuleId
                };

                var result = await _lessonRepository.AddAsync(lesson);

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
        public async Task<CusResponse<string>> UpdateLessonAsync(UpdateLessonRequest request)
        {
            try
            {
                var lesson = await _lessonRepository.GetByIdAsync(request.Id);
                if (lesson == null)
                {
                    return ErrorResponse($"Lesson with Id {request.Id} not found.", HttpStatusCode.NotFound);
                }

                lesson.Title = request.Title;
                lesson.Content = request.Content;
                lesson.ModuleId = request.ModuleId;

                if (request.videoFile != null)
                {
                    if (!string.IsNullOrEmpty(lesson.VideoUrl))
                    {
                        _videoService.DeleteVideo(lesson.VideoUrl);
                    }
                    lesson.VideoUrl = await _videoService.UploadVideoAsync(request.videoFile);
                }

                var result = await _lessonRepository.UpdateAsync(lesson);

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
        public async Task<CusResponse<string>> DeleteLessonAsync(int id)
        {
            try
            {
                var lesson = await _lessonRepository.GetByIdAsync(id);
                if (lesson == null)
                {
                    return ErrorResponse($"Lesson with Id {id} not found.", HttpStatusCode.NotFound);
                }


                if (!string.IsNullOrEmpty(lesson.VideoUrl))
                {
                    _videoService.DeleteVideo(lesson.VideoUrl);
                }

                var result = await _lessonRepository.DeleteAsync(lesson);

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
        public async Task<CusResponse<LessonResponseDto>> GetLessonByIdAsync(int id)
        {
            try
            {
                var lesson = await _lessonRepository.GetTableNoTracking()
                    .Include(l => l.Module)
                    .FirstOrDefaultAsync(l => l.LessonId == id);

                if (lesson == null)
                {
                    return new CusResponse<LessonResponseDto>
                    {
                        IsSuccess = true,
                        Message = $"Lessons with Id {id} not found.",
                        Data = null,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var lessonDto = new LessonResponseDto
                {
                    Id = lesson.LessonId,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    VideoUrl = lesson.VideoUrl,
                    ModuleName = lesson.Module.Title
                };

                return new CusResponse<LessonResponseDto>
                {
                    IsSuccess = true,
                    Message = "Lessons retrieved successfully.",
                    Data = lessonDto,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<LessonResponseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<LessonResponseDto>> GetLessonPaginatedListAsync(LessonPaginatedListRequest request)
        {
            var query = _lessonRepository.GetTableNoTracking().Include(x => x.Module)
                .Select(lesson => new LessonResponseDto
                {
                    Id = lesson.LessonId,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    VideoUrl = lesson.VideoUrl,
                    ModuleName = lesson.Module.Title
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<LessonResponseDto>(new List<LessonResponseDto>());
            }

            return await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
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