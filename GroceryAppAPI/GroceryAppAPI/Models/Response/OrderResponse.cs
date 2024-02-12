namespace GroceryAppAPI.Models.Response
{
    // Represents the order response.
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public IEnumerable<int>? ProductIds { get; set; }
    }
}
