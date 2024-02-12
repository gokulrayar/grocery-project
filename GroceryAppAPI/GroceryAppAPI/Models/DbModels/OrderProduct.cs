namespace GroceryAppAPI.Models.DbModels
{
    // Represents an orderid and productid.
    public class OrderProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
