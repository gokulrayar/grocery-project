namespace GroceryAppAPI.Models.Request
{
    // Represents an order placement request.
    public class OrderPlacementRequest
    {
        public PaymentRequest? PaymentRequest { get; set; }
        public OrderRequest? OrderRequest { get; set; }
    }
}
