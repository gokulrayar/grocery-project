using GroceryAppAPI.Attributes;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for managing user-related operations
    [Route("[controller]")]
    [ApiController]
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor to inject the user service dependency
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint for password reset (AllowAnonymous to allow unauthenticated access)
        [AllowAnonymous]
        [HttpPut("password")]
        public IActionResult ForgotPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            // Call the user service to update the user's password
            _userService.UpdatePassword(resetPasswordRequest);

            // Return Ok result with a success message
            return Ok(new { Message = "Password reset successful." });
        }

        // Endpoint to get user details (requires authorization)
        [Authorize] // Requires authorization
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            // Call the user service to get user details
            var userResponse = _userService.Get(id);

            // Return Ok result with the user details
            return Ok(new { data = userResponse });
        }

        // Endpoint to update user details (requires authorization)
        [Authorize] // Requires authorization
        [HttpPatch("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] string properties)
        {
            // Call the user service to update user details
            _userService.Update(id, properties);

            // Return Ok result with a success message
            return Ok(new { Message = "User details updated successfully." });
        }
    }
}
