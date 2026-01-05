using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public CourseService(ICourseRepository courseRepository, IMapper mapper, IUploadService uploadService)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public Task<bool> ChangeStatusAsync(long courseId, bool status)
        {
            return _courseRepository.ChangeStatusAsync(courseId, status);
        }

        public async Task<CourseResponse> CreateCourseAsync(CourseRequest request)
        {
            if (request.ThumbnailFile != null)
            {
                request.ThumbnailUrl = await _uploadService.UploadImageAsync(request.ThumbnailFile);
            }

            var newCourse = await _courseRepository.CreateCourseAsync(request);
            return _mapper.Map<CourseResponse>(newCourse);
        }

        public Task<bool> DeleteCourseAsync(long courseId)
        {
            return _courseRepository.DeleteCourseAsync(courseId);
        }

        public async Task<CourseResponse> GetCourseByIdAsync(long courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            return _mapper.Map<CourseResponse>(course);
        }

        public async Task<PagedResponse<CourseResponse>> GetPagedCoursesAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var pagedCourses = await _courseRepository.GetPagedCoursesAsync(keyword, isActive, page, pageSize);
            
            var mappedData = _mapper.Map<IEnumerable<CourseResponse>>(pagedCourses.Data);

            return new PagedResponse<CourseResponse>(
                mappedData,
                pagedCourses.TotalItems,
                pagedCourses.CurrentPage,
                pagedCourses.PageSize
            );
        }

        public async Task<CourseResponse> UpdateCourseAsync(long courseId, CourseRequest request)
        {
            if (request.ThumbnailFile != null)
            {
                request.ThumbnailUrl = await _uploadService.UploadImageAsync(request.ThumbnailFile);
            }

            var updatedCourse = await _courseRepository.UpdateCourseAsync(courseId, request);
            return _mapper.Map<CourseResponse>(updatedCourse);
        }
    }
}
