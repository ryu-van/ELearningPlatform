using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IVideoRepository
    {
        Task<PagedResponse<Video>> GetPagedVideosAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<IEnumerable<Video>> GetVideosByLessonIdAsync(long lessonId);
        Task<Video> GetVideoByIdAsync(long videoId);
        Task<Video> CreateVideoAsync(VideoRequest request);
        Task<Video> UpdateVideoAsync(long videoId, VideoRequest request);
        Task<bool> DeleteVideoAsync(long videoId);
        Task<bool> ChangeStatusAsync(long videoId, bool status);
    }
}
