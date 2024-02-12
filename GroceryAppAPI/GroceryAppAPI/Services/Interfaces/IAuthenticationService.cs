using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for an authentication service
    public interface IAuthenticationService
    {
        // Method to handle user login and return a response
        LoginResponse Login(LoginRequest loginRequest);
    }
}
