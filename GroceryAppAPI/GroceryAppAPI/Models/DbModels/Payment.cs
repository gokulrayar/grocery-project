namespace GroceryAppAPI.Models.DbModels
{
    // Represents a paymenttype and Amount.
    public class Payment : BaseEntity
    {
        public int Amount { get; set; }
        public int PaymentType { get; set; }
    }
}
