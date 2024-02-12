using GroceryAppAPI.Enumerations;
namespace GroceryAppAPI.Models.Request
{
    // Represents a payment request.
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
