using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;

namespace GroceryAppAPI.Services
{
    // Service for handling payments
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        // Constructor with dependency injection for IPaymentRepository
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // Add a new payment to the database
        public int Add(PaymentRequest paymentRequest)
        {
            // Validate the payment request
            Validate(paymentRequest);

            // Create a new payment object and add it to the repository
            var payment = new Payment() { Amount = paymentRequest.Amount, PaymentType = (int)paymentRequest.PaymentType };
            return _paymentRepository.Add(payment);
        }

        // Validate the payment request
        public void Validate(PaymentRequest paymentRequest)
        {
            // Check if the payment request is null
            if (paymentRequest is null) { throw new ArgumentNullException("Payment is either not given or invalid."); }

            // Check if the payment amount is valid
            if (paymentRequest.Amount <= 0) { throw new InvalidRequestDataException("Amount is either not given or invalid."); }

            // Check if the payment type is valid
            if (!Enum.IsDefined(typeof(PaymentType), paymentRequest.PaymentType)) { throw new InvalidRequestDataException("PaymentType is either not given or invalid."); }
        }
    }
}
