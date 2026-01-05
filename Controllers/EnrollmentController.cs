using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<EnrollmentResponse>>> GetEnrollments(
            [FromQuery] string? keyword,
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _enrollmentService.GetPagedEnrollmentsAsync(keyword ?? "", status ?? "", page, pageSize);
            return Ok(result);
        }

        [HttpGet("my-enrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EnrollmentResponse>>> GetMyEnrollments()
        {
            if (!TryGetUserId(out long userId))
            {
                return Unauthorized();
            }

            var result = await _enrollmentService.GetMyEnrollmentsAsync(userId);
            return Ok(result);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnrollmentResponse>> Enroll([FromBody] EnrollmentRequest request)
        {
            if (!TryGetUserId(out long userId))
            {
                return Unauthorized();
            }

            try 
            {
                var result = await _enrollmentService.EnrollUserAsync(userId, request.CourseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Unenroll(long id)
        {
            await _enrollmentService.UnenrollUserAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/progress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProgress(long id, [FromBody] decimal progress)
        {
            await _enrollmentService.UpdateProgressAsync(id, progress);
            return Ok(new { message = "Progress updated successfully" });
        }

        private bool TryGetUserId(out long userId)
        {
            userId = 0;
            var claim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return false;
            return long.TryParse(claim.Value, out userId);
        }
    }
}
