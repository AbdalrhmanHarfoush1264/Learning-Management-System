using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.CourseDTOS;
using LMSProject.Data.Bases;

namespace LMSProject.Bussiness.Interfaces
{
    public interface ICourseService
    {
        public Task<CusResponse<string>> AddCourseAsync(AddCourseRequest request);
        public Task<CusResponse<string>> UpdateCourseAsync(UpdateCourseRequest request);
        public Task<CusResponse<string>> DeleteCourseAsync(int courseId);
        public Task<CusResponse<List<CourseResponseDto>>> GetCoursesListAsync();
        public Task<PaginatedResult<CourseResponseDto>> GetCoursesPaginatedListAsync(CoursePaginatedListRequest request);
        public Task<CusResponse<CourseResponseDto>> GetCourseByIdAsync(int courseId);
    }
}
