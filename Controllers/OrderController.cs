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
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();

            try
            {
                var order = await _orderService.CreateOrderAsync(userId, request);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrderById(long id)
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            if (order.UserId != userId) return Forbid();

            return Ok(order);
        }

        [HttpGet("my-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetMyOrders()
        {
            if (!TryGetUserId(out long userId)) return Unauthorized();

            var orders = await _orderService.GetMyOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpPost("{id}/confirm")]
        [Authorize(Roles = "Admin,SuperAdmin,Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmOrder(long id)
        {
            if (!TryGetUserId(out long staffId)) return Unauthorized();

            try
            {
                var result = await _orderService.ConfirmOrderAsync(id, staffId);
                if (!result) return NotFound();
                return Ok(new { message = "Order confirmed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/pay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PayOrder(long id)
        {
            try
            {
                var result = await _orderService.MarkPaidAsync(id, "Paid", "Simulation", 0, "00", "Success");
                if (!result) return BadRequest(new { message = "Payment failed" });
                return Ok(new { message = "Order paid and enrollment activated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
