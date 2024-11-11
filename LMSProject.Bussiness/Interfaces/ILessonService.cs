using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.LessonDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ILessonService
    {
        public Task<CusResponse<string>> AddLessonAsync(AddLessonRequest request);
        public Task<CusResponse<string>> UpdateLessonAsync(UpdateLessonRequest request);
        public Task<CusResponse<string>> DeleteLessonAsync(int id);
        public Task<CusResponse<LessonResponseDto>> GetLessonByIdAsync(int id);
        public Task<PaginatedResult<LessonResponseDto>> GetLessonPaginatedListAsync(LessonPaginatedListRequest request);
    }
}
