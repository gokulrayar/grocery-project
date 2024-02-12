namespace GroceryAppAPI.Models.Response
{
    // Represents the cart response.
    public class CartResponse
    {
        public int CartId { get; set; }
        public IEnumerable<int>? ProductIds { get; set; }
    }
}
