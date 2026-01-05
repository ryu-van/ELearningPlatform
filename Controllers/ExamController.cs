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
    public class ExamController : ControllerBase
    {
        private readonly IExamService _service;
        public ExamController(IExamService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<ExamResponse>>> GetExams(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedExamsAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExamResponse>> GetExamById(long id)
        {
            var exam = await _service.GetExamByIdAsync(id);
            return Ok(exam);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ExamResponse>> CreateExam([FromBody] ExamRequest request)
        {
            var created = await _service.CreateExamAsync(request);
            return CreatedAtAction(nameof(GetExamById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExamResponse>> UpdateExam(long id, [FromBody] ExamRequest request)
        {
            var updated = await _service.UpdateExamAsync(id, request);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExam(long id)
        {
            await _service.DeleteExamAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeExamStatus(long id, [FromQuery] bool isActive)
        {
            await _service.ChangeStatusAsync(id, isActive);
            var statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Exam đã được {statusText} thành công." });
        }
    }
}
