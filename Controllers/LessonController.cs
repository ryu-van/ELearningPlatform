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
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<LessonResponse>>> GetLessons(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _lessonService.GetPagedLessonsAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LessonResponse>> GetLessonById(long id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            return Ok(lesson);
        }

        [HttpGet("chapter/{chapterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonResponse>>> GetLessonsByChapterId(long chapterId)
        {
            var lessons = await _lessonService.GetLessonsByChapterIdAsync(chapterId);
            return Ok(lessons);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<LessonResponse>> CreateLesson([FromBody] LessonRequest lessonRequest)
        {
            var newLesson = await _lessonService.CreateLessonAsync(lessonRequest);
            return CreatedAtAction(nameof(GetLessonById), new { id = newLesson.Id }, newLesson);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LessonResponse>> UpdateLesson(long id, [FromBody] LessonRequest lessonRequest)
        {
            var updatedLesson = await _lessonService.UpdateLessonAsync(id, lessonRequest);
            return Ok(updatedLesson);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLesson(long id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeLessonStatus(long id, [FromQuery] bool isActive)
        {
            await _lessonService.ChangeStatusAsync(id, isActive);
            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Lesson đã được {statusText} thành công." });
        }
    }
}
