namespace GroceryAppAPI.Models.DbModels
{
    // Represents an order.
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}