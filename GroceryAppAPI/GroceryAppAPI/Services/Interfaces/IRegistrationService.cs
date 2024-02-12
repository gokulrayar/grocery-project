using GroceryAppAPI.Models.Request;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for a registration service
    public interface IRegistrationService
    {
        // Method to register a new user based on the provided registration request
        void Register(RegistrationRequest registrationRequest);
    }
}
