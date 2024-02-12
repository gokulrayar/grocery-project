using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for handling authentication-related actions
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        // Constructor to inject the authentication service dependency
        public AuthenticationController(IAuthenticationService authenticationService)
        {   _authenticationService = authenticationService;  }

        // Endpoint for user login, allows anonymous access
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Call the authentication service to perform user login
            var loginResponse = _authenticationService.Login(loginRequest);
            // Return Ok result with the login response data
            return Ok(new { data = loginResponse });
        }
    }
}
