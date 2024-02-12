using GroceryAppAPI.Enumerations;
namespace GroceryAppAPI.Models.Request
{
    // Represents a cart request.
    public class CartRequest
    {
        public int ProductId { get; set; }
        public CartOperationType OperationType { get; set; }
    }
}
