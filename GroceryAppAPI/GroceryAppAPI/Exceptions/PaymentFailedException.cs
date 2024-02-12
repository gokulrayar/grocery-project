namespace GroceryAppAPI.Exceptions
{
    // Represents an error which occurs when the payment for an order fails.
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(string message)
            : base("Payment failed for the order. Order cannot be placed. " + message)
        {
        }
    }
}
