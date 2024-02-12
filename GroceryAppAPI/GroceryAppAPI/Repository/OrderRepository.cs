using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to orders
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        // Constructor to set the database connection using dependency injection
        public OrderRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to add a new order to the database
        public int Add(Order order)
        {
            const string query = @"INSERT INTO [Orders] ([UserId], [OrderedAt])
                                   OUTPUT INSERTED.Id
                                   VALUES (@UserId, GETUTCDATE())";
            return Add(query, order);
        }

        // Method to delete an order from the database by ID
        public void Delete(int id)
        {
            const string query = @"DELETE FROM [Orders]
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            Delete(query, parameters);
        }

        // Method to get all orders from the database by user ID with a specified payment ID
        public IEnumerable<Order> GetAll(int userId)
        {
            const string query = @"SELECT *
                                   FROM [Orders]
                                   WHERE [UserId] = @UserId
                                   AND [PaymentId] IS NOT NULL";
            var parameters = new { UserId = userId };
            return GetAll(query, parameters);
        }

        // Method to update the payment ID for an order in the database by ID
        public void Update(int id, int paymentId)
        {
            const string query = @"UPDATE [Orders]
                                   SET [PaymentId] = @PaymentId
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id, PaymentId = paymentId };
            Update(query, parameters);
        }
    }
}
