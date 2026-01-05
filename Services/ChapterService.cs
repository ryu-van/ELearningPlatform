using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IMapper _mapper;

        public ChapterService(IChapterRepository chapterRepository, IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }

        public Task<bool> ChangeStatusAsync(long chapterId, bool status)
        {
            return _chapterRepository.ChangeStatusAsync(chapterId, status);
        }

        public async Task<ChapterResponse> CreateChapterAsync(ChapterRequest request)
        {
            var newChapter = await _chapterRepository.CreateChapterAsync(request);
            return _mapper.Map<ChapterResponse>(newChapter);
        }

        public Task<bool> DeleteChapterAsync(long chapterId)
        {
            return _chapterRepository.DeleteChapterAsync(chapterId);
        }

        public async Task<ChapterResponse> GetChapterByIdAsync(long chapterId)
        {
            var chapter = await _chapterRepository.GetChapterByIdAsync(chapterId);
            return _mapper.Map<ChapterResponse>(chapter);
        }

        public async Task<IEnumerable<ChapterResponse>> GetChaptersByCourseIdAsync(long courseId)
        {
            var chapters = await _chapterRepository.GetChaptersByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<ChapterResponse>>(chapters);
        }

        public async Task<PagedResponse<ChapterResponse>> GetPagedChaptersAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var pagedChapters = await _chapterRepository.GetPagedChaptersAsync(keyword, isActive, page, pageSize);
            
            var mappedData = _mapper.Map<IEnumerable<ChapterResponse>>(pagedChapters.Data);

            return new PagedResponse<ChapterResponse>(
                mappedData,
                pagedChapters.TotalItems,
                pagedChapters.CurrentPage,
                pagedChapters.PageSize
            );
        }

        public async Task<ChapterResponse> UpdateChapterAsync(long chapterId, ChapterRequest request)
        {
            var updatedChapter = await _chapterRepository.UpdateChapterAsync(chapterId, request);
            return _mapper.Map<ChapterResponse>(updatedChapter);
        }
    }
}
