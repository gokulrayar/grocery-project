using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using System.Collections.Generic;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for an order service
    public interface IOrderService
    {
        // Method to get all orders for a specific user
        IEnumerable<OrderResponse> GetAll(int userId);

        // Method to place a new order for a user and return a response
        OrderPlacementResponse Place(int userId, OrderPlacementRequest paymentRequest);
    }
}
