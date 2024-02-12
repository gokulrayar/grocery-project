using GroceryAppAPI.Attributes;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for handling operations related to user shopping carts
    [Route("users/{userId:int}/[controller]")]
    [ApiController]
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        // Constructor to inject the cart service dependency
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Endpoint to add a new cart for a user
        [Authorize] // Requires authorization
        [HttpPost]
        public IActionResult Post([FromRoute] int userId, [FromBody] CartRequest cartRequest)
        {
            // Call the cart service to add a new cart for the user
            var id = _cartService.Add(userId, cartRequest);

            // Return Ok result with the newly created cart's ID
            return Ok(new { data = new { Id = id } });
        }

        // Endpoint to get the user's cart
        [Authorize] // Requires authorization
        [HttpGet]
        public IActionResult Get([FromRoute] int userId)
        {
            // Call the cart service to get the user's cart
            var cart = _cartService.Get(userId);

            // Check if the cart is null and return a message if the user does not have any cart
            if (cart is null)
            {
                return Ok(new { Message = "User does not have any cart." });
            }

            // Return Ok result with the user's cart
            return Ok(new { data = cart });
        }

        // Endpoint to update a user's cart
        [Authorize] // Requires authorization
        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromRoute] int userId, [FromBody] CartRequest cartRequest)
        {
            // Call the cart service to update the user's cart
            _cartService.Update(id, userId, cartRequest);

            // Return Ok result with a success message
            return Ok(new { Message = "Cart updated successfully." });
        }

        // Endpoint to delete a user's cart
        [Authorize] // Requires authorization
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromRoute] int userId)
        {
            // Call the cart service to delete the user's cart
            _cartService.Delete(id, userId);

            // Return Ok result with a success message
            return Ok(new { Message = "Cart deleted successfully." });
        }
    }
}
