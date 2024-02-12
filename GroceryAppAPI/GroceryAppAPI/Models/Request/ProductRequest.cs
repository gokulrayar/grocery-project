using GroceryAppAPI.Enumerations;
namespace GroceryAppAPI.Models.Request
{
    // Represents a product request.
    public class ProductRequest
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Existing;
    }
}
