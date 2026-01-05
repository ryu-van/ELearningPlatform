using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;

namespace E_learning_platform.Services
{
    public interface IVideoService
    {
        Task<PagedResponse<VideoResponse>> GetPagedVideosAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<VideoResponse>> GetVideosByLessonIdAsync(long lessonId);
        Task<VideoResponse> GetVideoByIdAsync(long videoId);
        Task<VideoResponse> CreateVideoAsync(VideoRequest request);
        Task<VideoResponse> UpdateVideoAsync(long videoId, VideoRequest request);
        Task<bool> DeleteVideoAsync(long videoId);
        Task<bool> ChangeStatusAsync(long videoId, bool status);
    }
}
