using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public Task<bool> ChangeStatusAsync(long lessonId, bool status)
        {
            return _lessonRepository.ChangeStatusAsync(lessonId, status);
        }

        public async Task<LessonResponse> CreateLessonAsync(LessonRequest request)
        {
            var newLesson = await _lessonRepository.CreateLessonAsync(request);
            return _mapper.Map<LessonResponse>(newLesson);
        }

        public Task<bool> DeleteLessonAsync(long lessonId)
        {
            return _lessonRepository.DeleteLessonAsync(lessonId);
        }

        public async Task<LessonResponse> GetLessonByIdAsync(long lessonId)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(lessonId);
            return _mapper.Map<LessonResponse>(lesson);
        }

        public async Task<IEnumerable<LessonResponse>> GetLessonsByChapterIdAsync(long chapterId)
        {
            var lessons = await _lessonRepository.GetLessonsByChapterIdAsync(chapterId);
            return _mapper.Map<IEnumerable<LessonResponse>>(lessons);
        }

        public async Task<PagedResponse<LessonResponse>> GetPagedLessonsAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var pagedLessons = await _lessonRepository.GetPagedLessonsAsync(keyword, isActive, page, pageSize);
            
            var mappedData = _mapper.Map<IEnumerable<LessonResponse>>(pagedLessons.Data);

            return new PagedResponse<LessonResponse>(
                mappedData,
                pagedLessons.TotalItems,
                pagedLessons.CurrentPage,
                pagedLessons.PageSize
            );
        }

        public async Task<LessonResponse> UpdateLessonAsync(long lessonId, LessonRequest request)
        {
            var updatedLesson = await _lessonRepository.UpdateLessonAsync(lessonId, request);
            return _mapper.Map<LessonResponse>(updatedLesson);
        }
    }
}
