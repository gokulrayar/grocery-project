namespace GroceryAppAPI.Models.DbModels
{
    // Represents a product.
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public int Status { get; set; }
    }
}