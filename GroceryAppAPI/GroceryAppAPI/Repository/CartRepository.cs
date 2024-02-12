using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to user shopping carts
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        // Constructor to set the database connection using dependency injection
        public CartRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to add a new cart to the database
        public int Add(Cart cart)
        {
            const string query = @"INSERT INTO [Carts] ([UserId])
                                   OUTPUT INSERTED.Id
                                   VALUES (@UserId)";
            return Add(query, cart);
        }

        // Method to delete a cart from the database by ID
        public void Delete(int id)
        {
            const string query = @"DELETE FROM [Carts] 
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            Update(query, parameters);
        }

        // Method to get a cart from the database by ID
        public Cart Get(int id)
        {
            const string query = @"SELECT * 
                                   FROM [Carts]
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            return GetAll(query, parameters).FirstOrDefault();
        }

        // Method to get a user's cart from the database by user ID
        public Cart GetByUser(int userId)
        {
            string query = @"SELECT * 
                             FROM [Carts]
                             WHERE [UserId] = @UserId";
            var parameters = new { UserId = userId };
            return GetAll(query, parameters).FirstOrDefault();
        }
    }
}
