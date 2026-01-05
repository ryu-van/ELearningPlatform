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
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<BranchResponse>>> GetBranchPage(
            [FromQuery] string? keyword,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _branchService.GetPageOfBranchAsync(keyword, isActive, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BranchResponse>> GetBranchById(long id)
        {
            var branch = await _branchService.GetBranchById(id);
            if (branch == null)
                return NotFound(new { message = $"Branch với ID {id} không tồn tại" });

            return Ok(branch);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BranchResponse>> CreateBranch([FromBody] BranchRequest branchRequest)
        {
            var newBranch = await _branchService.CreateNewBranchAsync(branchRequest);
            return CreatedAtAction(nameof(GetBranchById), new { id = newBranch.Id }, newBranch);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BranchResponse>> UpdateBranch(long id, [FromBody] BranchRequest branchRequest)
        {
            var updatedBranch = await _branchService.UpdateBranch(id, branchRequest);
            if (updatedBranch == null)
                return NotFound(new { message = $"Branch với ID {id} không tồn tại" });

            return Ok(updatedBranch);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBranch(long id)
        {
            var result = await _branchService.DeleteBranchAsync(id);
            if (!result)
                return NotFound(new { message = $"Branch với ID {id} không tồn tại" });

            return NoContent();
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeBranchStatus(long id, [FromQuery] bool isActive)
        {
            bool result = await _branchService.ChangeBranchStatusAsync(id, isActive);

            if (!result)
                return NotFound(new { message = $"Branch với ID {id} không tồn tại" });

            string statusText = isActive ? "kích hoạt" : "vô hiệu hóa";
            return Ok(new { message = $"Branch đã được {statusText} thành công." });
        }
    }
}
