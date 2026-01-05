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
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _service;
        public LevelController(ILevelService service)
        {
            _service = service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<LevelResponse>>> GetLevels([FromQuery] string? keyword, [FromQuery] bool? isActive, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LevelResponse>> GetById(long id)
        {
            var entity = await _service.GetByIdAsync(id);
            return Ok(entity);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<LevelResponse>> Create([FromBody] LevelRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LevelResponse>> Update(long id, [FromBody] LevelRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeStatus(long id, [FromQuery] bool isActive)
        {
            await _service.ChangeStatusAsync(id, isActive);
            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Level đã được {statusText} thành công." });
        }
    }
}
