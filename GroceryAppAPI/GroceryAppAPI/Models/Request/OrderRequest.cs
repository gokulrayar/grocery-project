namespace GroceryAppAPI.Models.Request
{
    // Represents an order request.
    public class OrderRequest
    {
        public IEnumerable<int>? ProductIds { get; set; }
    }
}
