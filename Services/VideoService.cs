using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public VideoService(IVideoRepository videoRepository, IMapper mapper, IUploadService uploadService)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<bool> ChangeStatusAsync(long videoId, bool status)
        {
            return await _videoRepository.ChangeStatusAsync(videoId, status);
        }

        public async Task<VideoResponse> CreateVideoAsync(VideoRequest request)
        {
            if (request.VideoFile != null)
            {
                request.Url = await _uploadService.UploadVideoAsync(request.VideoFile);
            }
            else if (string.IsNullOrEmpty(request.Url))
            {
                throw new ArgumentException("Video File or URL is required");
            }

            if (request.ThumbnailFile != null)
            {
                request.ThumbnailUrl = await _uploadService.UploadImageAsync(request.ThumbnailFile);
            }

            var newVideo = await _videoRepository.CreateVideoAsync(request);
            return _mapper.Map<VideoResponse>(newVideo);
        }

        public async Task<bool> DeleteVideoAsync(long videoId)
        {
            return await _videoRepository.DeleteVideoAsync(videoId);
        }

        public async Task<PagedResponse<VideoResponse>> GetPagedVideosAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var pagedVideos = await _videoRepository.GetPagedVideosAsync(keyword, isActive, page, pageSize);
            
            var mappedData = _mapper.Map<IEnumerable<VideoResponse>>(pagedVideos.Data);

            return new PagedResponse<VideoResponse>(
                mappedData,
                pagedVideos.TotalItems,
                pagedVideos.CurrentPage,
                pagedVideos.PageSize
            );
        }

        public async Task<VideoResponse> GetVideoByIdAsync(long videoId)
        {
            var video = await _videoRepository.GetVideoByIdAsync(videoId);
            return _mapper.Map<VideoResponse>(video);
        }

        public async Task<IEnumerable<VideoResponse>> GetVideosByLessonIdAsync(long lessonId)
        {
            var videos = await _videoRepository.GetVideosByLessonIdAsync(lessonId);
            return _mapper.Map<IEnumerable<VideoResponse>>(videos);
        }

        public async Task<VideoResponse> UpdateVideoAsync(long videoId, VideoRequest request)
        {
            if (request.VideoFile != null)
            {
                request.Url = await _uploadService.UploadVideoAsync(request.VideoFile);
            }

            if (request.ThumbnailFile != null)
            {
                request.ThumbnailUrl = await _uploadService.UploadImageAsync(request.ThumbnailFile);
            }

            var updatedVideo = await _videoRepository.UpdateVideoAsync(videoId, request);
            return _mapper.Map<VideoResponse>(updatedVideo);
        }
    }
}
