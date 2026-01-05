using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _service;
        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<QuestionResponse>>> GetQuestions(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedQuestionsAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuestionResponse>> GetQuestionById(long id)
        {
            var item = await _service.GetQuestionByIdAsync(id);
            return Ok(item);
        }

        [HttpGet("section/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QuestionResponse>>> GetQuestionsBySectionId(long sectionId)
        {
            var items = await _service.GetQuestionsBySectionIdAsync(sectionId);
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<QuestionResponse>> CreateQuestion([FromBody] QuestionRequest request)
        {
            var created = await _service.CreateQuestionAsync(request);
            return CreatedAtAction(nameof(GetQuestionById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuestionResponse>> UpdateQuestion(long id, [FromBody] QuestionRequest request)
        {
            var updated = await _service.UpdateQuestionAsync(id, request);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuestion(long id)
        {
            await _service.DeleteQuestionAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeQuestionStatus(long id, [FromQuery] bool isActive)
        {
            await _service.ChangeStatusAsync(id, isActive);
            var statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Question đã được {statusText} thành công." });
        }
    }
}
