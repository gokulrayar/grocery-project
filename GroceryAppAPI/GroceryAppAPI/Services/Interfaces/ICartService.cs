using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for a shopping cart service
    public interface ICartService
    {
        // Method to get a user's shopping cart by userId
        CartResponse Get(int userId);

        // Method to add a new cart for a user and return the cart's ID
        int Add(int userId, CartRequest cartRequest);

        // Method to delete a cart by ID for a specific user
        void Delete(int id, int userId);

        // Method to update a cart by ID for a specific user
        void Update(int id, int userId, CartRequest cartRequest);
    }
}
