using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for handling CartProduct-related database operations
    public class CartProductRepository : BaseRepository<CartProduct>, ICartProductRepository
    {
        // Constructor with dependency injection for connection string options
        public CartProductRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Delete a specific CartProduct by CartId and ProductId
        public void Delete(int cartId, int productId)
        {
            const string query = @"DELETE FROM [Carts_Products] 
                                   WHERE [CartId] = @CartId
                                   AND [ProductId] = @ProductId";
            var parameters = new { CartId = cartId, ProductId = productId };
            Update(query, parameters);
        }

        // Get all CartProducts for a specific Cart by CartId
        public IEnumerable<CartProduct> GetAll(int cartId)
        {
            const string query = @"SELECT *
                                   FROM [Carts_Products]
                                   WHERE [CartId] = @CartId";
            var parameters = new { CartId = cartId };
            return GetAll(query, parameters);
        }

        // Add a new CartProduct to the database
        public void Add(CartProduct cartProduct)
        {
            const string query = @"INSERT INTO [Carts_Products] ([CartId], [ProductId])
                                   OUTPUT INSERTED.Id
                                   VALUES (@CartId, @ProductId)";
            Add(query, cartProduct);
        }

        // Delete all CartProducts for a specific Cart by CartId
        public void Delete(int cartId)
        {
            const string query = @"DELETE FROM [Carts_Products]
                                   WHERE [CartId] = @CartId";
            var parameters = new { CartId = cartId };
            Delete(query, parameters);
        }
    }
}
