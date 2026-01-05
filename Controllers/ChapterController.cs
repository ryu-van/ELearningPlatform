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
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<ChapterResponse>>> GetChapters(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _chapterService.GetPagedChaptersAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterResponse>> GetChapterById(long id)
        {
            var chapter = await _chapterService.GetChapterByIdAsync(id);
            return Ok(chapter);
        }

        [HttpGet("course/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChapterResponse>>> GetChaptersByCourseId(long courseId)
        {
            var chapters = await _chapterService.GetChaptersByCourseIdAsync(courseId);
            return Ok(chapters);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ChapterResponse>> CreateChapter([FromBody] ChapterRequest chapterRequest)
        {
            var newChapter = await _chapterService.CreateChapterAsync(chapterRequest);
            return CreatedAtAction(nameof(GetChapterById), new { id = newChapter.Id }, newChapter);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterResponse>> UpdateChapter(long id, [FromBody] ChapterRequest chapterRequest)
        {
            var updatedChapter = await _chapterService.UpdateChapterAsync(id, chapterRequest);
            return Ok(updatedChapter);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteChapter(long id)
        {
            await _chapterService.DeleteChapterAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeChapterStatus(long id, [FromQuery] bool isActive)
        {
            await _chapterService.ChangeStatusAsync(id, isActive);
            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Chapter đã được {statusText} thành công." });
        }
    }
}
