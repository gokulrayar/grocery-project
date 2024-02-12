using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for user registration
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;

        // Constructor to inject the registration service dependency
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // Endpoint for user registration (AllowAnonymous to allow unauthenticated access)
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration([FromBody] RegistrationRequest registrationRequest)
        {
            // Call the registration service to register a new user
            _registrationService.Register(registrationRequest);

            // Return Ok result with a success message
            return Ok(new { Message = "User registered successfully." });
        }
    }
}
