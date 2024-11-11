using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ModuleDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class ModuleService : IModuleService
    {
        #region Fileds
        private readonly IGenericRepository<Module> _moduleRepository;
        private readonly ICourseService _courseService;
        #endregion

        #region Constructors
        public ModuleService(IGenericRepository<Module> moduleRepository, ICourseService courseService)
        {
            _moduleRepository = moduleRepository;
            _courseService = courseService;
        }
        #endregion


        #region Public-Methods
        public async Task<CusResponse<string>> AddModuleAsync(AddModuleRequest request)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"Course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                var module = new Module();
                module.CourseId = request.CourseId;
                module.Title = request.Title;
                module.Description = request.Description;


                var result = await _moduleRepository.AddAsync(module);

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
        public async Task<CusResponse<string>> UpdateModuleAsync(UpdateModuleRequest request)
        {

            try
            {
                var module = await _moduleRepository.GetByIdAsync(request.Id);
                if (module == null)
                {
                    return ErrorResponse($"Module with Id : {request.Id} not found!", HttpStatusCode.NotFound);
                }

                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return ErrorResponse($"Course with Id : {request.CourseId} not found!", HttpStatusCode.NotFound);
                }

                module.CourseId = request.CourseId;
                module.Title = request.Title;
                module.Description = request.Description;

                var result = await _moduleRepository.UpdateAsync(module);

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
        public async Task<CusResponse<string>> DeleteModuleAsync(int moduleId)
        {

            try
            {
                var module = await _moduleRepository.GetByIdAsync(moduleId);
                if (module == null)
                {
                    return ErrorResponse($"Module with Id : {moduleId} not found!", HttpStatusCode.NotFound);
                }


                var result = await _moduleRepository.DeleteAsync(module);

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
        public async Task<CusResponse<List<ModuleResponseDto>>> GetNotificationListAsync()
        {
            try
            {
                var resultList = await _moduleRepository.GetTableNoTracking()
                    .Include(x => x.Course).ToListAsync();

                var moduleListDto = new List<ModuleResponseDto>();

                foreach (var item in resultList)
                {

                    var moduleDto = new ModuleResponseDto
                    {
                        Id = item.ModuleId,
                        Title = item.Title,
                        Description = item.Description ?? "no Found.",
                        CourseName = item.Course.Title
                    };
                    moduleListDto.Add(moduleDto);
                }

                return new CusResponse<List<ModuleResponseDto>>
                {
                    IsSuccess = true,
                    Message = moduleListDto.Any() ? "Modules retrieved successfully." : "No modules available.",
                    Data = moduleListDto,
                    DataCount = moduleListDto.Count,
                    StatusCode = moduleListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<ModuleResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<ModuleResponseDto>> GetModulePaginatedListAsync(ModulePaginatedListRequest request)
        {
            var query = _moduleRepository.GetTableNoTracking().Include(x => x.Course)
                .Select(Module => new ModuleResponseDto
                {
                    Id = Module.ModuleId,
                    Title = Module.Title,
                    Description = Module.Description ?? "no Found.",
                    CourseName = Module.Course.Title
                })
                .AsQueryable();

            if (!query.Any())
            {
                return new PaginatedResult<ModuleResponseDto>(new List<ModuleResponseDto>());
            }

            var paginated = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);


            return paginated;
        }
        public async Task<CusResponse<ModuleResponseDto>> GetNotificationByIdAsync(int moduleId)
        {
            try
            {
                var module = await _moduleRepository.GetTableNoTracking()
                    .Where(x => x.ModuleId == moduleId)
                    .Include(x => x.Course).FirstOrDefaultAsync();

                if (module == null)
                {
                    return new CusResponse<ModuleResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"Module with Id : {moduleId} not found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var moduleDto = new ModuleResponseDto
                {
                    Id = module.ModuleId,
                    Title = module.Title,
                    Description = module.Description ?? "no Found.",
                    CourseName = module.Course.Title
                };

                return new CusResponse<ModuleResponseDto>
                {
                    IsSuccess = true,
                    Message = "Modules retrieved successfully.",
                    Data = moduleDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<ModuleResponseDto>
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
