namespace GroceryAppAPI.Models.DbModels
{
    // Represents a cartid and productid.
    public class CartProduct : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}
