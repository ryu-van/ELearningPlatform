using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace E_learning_platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("course/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<ReviewResponse>>> GetCourseReviews(
            long courseId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _reviewService.GetReviewsByCourseIdAsync(courseId, page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewResponse>> AddReview([FromBody] ReviewRequest request)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();

            try
            {
                var result = await _reviewService.AddReviewAsync(userId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteReview(long id)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = role == "Admin" || role == "SuperAdmin"; 

            try 
            {
                var success = await _reviewService.DeleteReviewAsync(id, userId, isAdmin);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
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
