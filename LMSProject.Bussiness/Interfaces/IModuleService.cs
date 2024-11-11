using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.ModuleDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface IModuleService
    {
        public Task<CusResponse<string>> AddModuleAsync(AddModuleRequest request);
        public Task<CusResponse<string>> UpdateModuleAsync(UpdateModuleRequest request);
        public Task<CusResponse<string>> DeleteModuleAsync(int moduleId);
        public Task<CusResponse<List<ModuleResponseDto>>> GetNotificationListAsync();
        public Task<PaginatedResult<ModuleResponseDto>> GetModulePaginatedListAsync(ModulePaginatedListRequest request);
        public Task<CusResponse<ModuleResponseDto>> GetNotificationByIdAsync(int moduleId);
    }
}
