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
    public class ExamAttemptController : ControllerBase
    {
        private readonly IExamAttemptService _attemptService;

        public ExamAttemptController(IExamAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExamAttemptResponse>> Start([FromBody] StartExamAttemptRequest request)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            var result = await _attemptService.StartAttemptAsync(userId, request);
            return Ok(result);
        }

        [HttpPost("submit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Submit([FromBody] SubmitAnswerRequest request)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            await _attemptService.SubmitAnswerAsync(userId, request);
            return Ok(new { message = "Đã lưu câu trả lời" });
        }

        [HttpPost("{attemptId}/finish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExamAttemptResponse>> Finish(long attemptId)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            var result = await _attemptService.FinishAttemptAsync(userId, attemptId);
            return Ok(result);
        }

        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ExamAttemptResponse>>> GetMy([FromQuery] long? examId)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            var result = await _attemptService.GetMyAttemptsAsync(userId, examId);
            return Ok(result);
        }

        [HttpGet("{attemptId}/answers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UserAnswerResponse>>> Answers(long attemptId)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();
            var result = await _attemptService.GetAttemptAnswersAsync(userId, attemptId);
            return Ok(result);
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
