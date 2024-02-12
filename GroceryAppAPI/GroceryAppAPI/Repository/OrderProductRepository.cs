using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to order products
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        // Constructor to set the database connection using dependency injection
        public OrderProductRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to add a new order product to the database
        public int Add(OrderProduct orderProduct)
        {
            const string query = @"INSERT INTO [Orders_Products] ([OrderId], [ProductId])
                                   OUTPUT INSERTED.Id
                                   VALUES (@OrderId, @ProductId)";
            return Add(query, orderProduct);
        }

        // Method to get all order products from the database by order ID
        public IEnumerable<OrderProduct> GetAll(int orderId)
        {
            const string query = @"SELECT *
                                   FROM [Orders_Products]
                                   WHERE [OrderId] = @OrderId";
            var parameters = new { OrderId = orderId };
            return GetAll(query, parameters);
        }

        // Method to delete all order products from the database by order ID
        public void Delete(int orderId)
        {
            const string query = @"DELETE FROM [Orders_Products] 
                                   WHERE [OrderId] = @OrderId";
            var parameters = new { OrderId = orderId };
            Delete(query, parameters);
        }
    }
}
