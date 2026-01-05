using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Helpers;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusAsync(long videoId, bool status)
        {
            var existingVideo = await _context.Videos
                .FirstOrDefaultAsync(v => v.Id == videoId);

            if (existingVideo == null)
            {
                throw new EntityNotFoundException("Video", new[] { videoId });
            }

            existingVideo.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Video> CreateVideoAsync(VideoRequest request)
        {
            var video = new Video
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Url = request.Url,
                Duration = request.Duration,
                FileSize = request.FileSize,
                Resolution = request.Resolution,
                ThumbnailUrl = request.ThumbnailUrl,
                IsActive = request.IsActive,
                LessonId = request.LessonId,
                LanguageId = request.LanguageId,
                LevelId = request.LevelId,
                ViewCount = 0
            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<bool> DeleteVideoAsync(long videoId)
        {
            var existingVideo = await _context.Videos
                .FirstOrDefaultAsync(v => v.Id == videoId);

            if (existingVideo == null)
            {
                throw new EntityNotFoundException("Video", new[] { videoId });
            }

            _context.Videos.Remove(existingVideo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Video> GetVideoByIdAsync(long videoId)
        {
            var existingVideo = await _context.Videos
                .Include(v => v.Lesson)
                .Include(v => v.Language)
                .Include(v => v.Level)
                .FirstOrDefaultAsync(v => v.Id == videoId);

            if (existingVideo == null)
            {
                throw new EntityNotFoundException("Video", new[] { videoId });
            }

            return existingVideo;
        }

        public async Task<IEnumerable<Video>> GetVideosByLessonIdAsync(long lessonId)
        {
            return await _context.Videos
                .Where(v => v.LessonId == lessonId)
                .Include(v => v.Language)
                .Include(v => v.Level)
                .OrderBy(v => v.Id)
                .ToListAsync();
        }

        public async Task<PagedResponse<Video>> GetPagedVideosAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Videos
                .Include(v => v.Lesson)
                .Include(v => v.Language)
                .Include(v => v.Level)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(v => EF.Functions.Like(v.Title, $"%{keyword}%"));
            }

            if (isActive.HasValue)
            {
                query = query.Where(v => v.IsActive == isActive.Value);
            }

            query = query.OrderBy(v => v.Id);

            return await query.ToPagedResponseAsync(page, pageSize);
        }

        public async Task<Video> UpdateVideoAsync(long videoId, VideoRequest request)
        {
             var existingVideo = await _context.Videos
                .FirstOrDefaultAsync(v => v.Id == videoId);

            if (existingVideo == null)
            {
                throw new EntityNotFoundException("Video", new[] { videoId });
            }

            existingVideo.Title = request.Title;
            existingVideo.Description = request.Description;
            existingVideo.Type = request.Type;
            existingVideo.Url = request.Url;
            existingVideo.Duration = request.Duration;
            existingVideo.FileSize = request.FileSize;
            existingVideo.Resolution = request.Resolution;
            existingVideo.ThumbnailUrl = request.ThumbnailUrl;
            existingVideo.IsActive = request.IsActive;
            existingVideo.LessonId = request.LessonId;
            existingVideo.LanguageId = request.LanguageId;
            existingVideo.LevelId = request.LevelId;

            await _context.SaveChangesAsync();
            return existingVideo;
        }
    }
}
