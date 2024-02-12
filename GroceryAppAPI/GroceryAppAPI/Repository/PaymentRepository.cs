using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to payments
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        // Constructor to set the database connection using dependency injection
        public PaymentRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to add a new payment to the database
        public int Add(Payment payment)
        {
            const string query = @"INSERT INTO [Payments] ([Amount], [PaymentType])
                                   OUTPUT INSERTED.Id
                                   VALUES (@Amount, @PaymentType)";
            return Add(query, payment);
        }

        // Method to get a payment from the database by ID
        public Payment Get(int id)
        {
            const string query = @"SELECT * FROM 
                                   [Payments] WHERE [Id] = @Id";
            var parameters = new { Id = id };
            return GetAll(query, parameters).FirstOrDefault();
        }
    }
}
