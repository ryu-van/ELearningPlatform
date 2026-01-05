using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<VideoResponse>>> GetVideos(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _videoService.GetPagedVideosAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoResponse>> GetVideoById(long id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            return Ok(video);
        }

        [HttpGet("lesson/{lessonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VideoResponse>>> GetVideosByLessonId(long lessonId)
        {
            var videos = await _videoService.GetVideosByLessonIdAsync(lessonId);
            return Ok(videos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<VideoResponse>> CreateVideo([FromForm] VideoRequest videoRequest)
        {
            var newVideo = await _videoService.CreateVideoAsync(videoRequest);
            return CreatedAtAction(nameof(GetVideoById), new { id = newVideo.Id }, newVideo);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoResponse>> UpdateVideo(long id, [FromForm] VideoRequest videoRequest)
        {
            var updatedVideo = await _videoService.UpdateVideoAsync(id, videoRequest);
            return Ok(updatedVideo);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVideo(long id)
        {
            await _videoService.DeleteVideoAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeVideoStatus(long id, [FromQuery] bool isActive)
        {
            await _videoService.ChangeStatusAsync(id, isActive);
            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Video đã được {statusText} thành công." });
        }
    }
}
