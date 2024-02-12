using GroceryAppAPI.Enumerations;

namespace GroceryAppAPI.Models.Response
{
    // Represents the product response.
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public ProductStatus Status { get; set; }
    }
}
