using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for a payment service
    public interface IPaymentService
    {
        // Method to add a new payment and return the payment ID
        int Add(PaymentRequest paymentRequest);
    }
}
