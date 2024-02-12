using GroceryAppAPI.Attributes;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for handling user orders
    [Route("users/{userId:int}/[controller]")]
    [ApiController]
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor to inject the order service dependency
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Endpoint to get all orders for a user
        [Authorize] // Requires authorization
        [HttpGet]
        public IActionResult Get([FromRoute] int userId)
        {
            // Call the order service to get all orders for the specified user
            var orders = _orderService.GetAll(userId);

            // Return Ok result with the list of orders
            return Ok(orders);
        }

        // Endpoint to place a new order for a user
        [Authorize] // Requires authorization
        [HttpPost("place")]
        public IActionResult Payment([FromRoute] int userId, [FromBody] OrderPlacementRequest placementRequest)
        {
            // Call the order service to place a new order for the specified user
            var placementResponse = _orderService.Place(userId, placementRequest);

            // Return Ok result with the placement response data
            return Ok(new { data = placementResponse });
        }
    }
}
