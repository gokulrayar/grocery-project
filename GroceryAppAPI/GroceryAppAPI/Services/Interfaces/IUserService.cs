using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for a user service
    public interface IUserService
    {
        // Method to get a user by ID
        UserResponse Get(int id);

        // Method to update user details by ID based on specified properties
        void Update(int id, string properties);

        // Method to update user password based on the provided reset password request
        void UpdatePassword(ResetPasswordRequest resetPasswordRequest);
    }
}
