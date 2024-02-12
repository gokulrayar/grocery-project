using GroceryAppAPI.Configurations;
using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to products
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        // Constructor to set the database connection using dependency injection
        public ProductRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to add a new product to the database
        public int Add(Product product)
        {
            const string query = @"INSERT INTO [Products] ([Name], [Price], [Stock], [ImageUrl], [Status])
                                   OUTPUT INSERTED.Id
                                   VALUES (@Name, @Price, @Stock, @ImageUrl, @Status)";
            var parameters = new
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            };
            return Add(query, parameters);
        }

        // Method to delete a product from the database by ID
        public void Delete(int id)
        {
            const string query = @"DELETE FROM [Products] 
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            Delete(query, parameters);
        }

        // Method to get a product from the database by ID
        public Product Get(int id)
        {
            const string query = @"SELECT * 
                                   FROM [Products]
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            return Get(query, parameters);
        }

        // Method to get all products from the database
        public IEnumerable<Product> GetAll()
        {
            const string query = @"SELECT *
                                   FROM [Products]";
            return GetAll(query);
        }

        // Method to update a product in the database based on specified conditions
        public void Update(string conditions, Product product)
        {
            string query = @$"UPDATE [Products] 
                                   SET {conditions}
                                   WHERE [Id] = @Id";
            Update(query, product);
        }

        // Method to update the status of a product to "Removed" in the database by ID
        public void UpdateStatusAsRemoved(int id)
        {
            const string query = @"UPDATE [Products]
                                   SET [Status] = @Status
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id, Status = (int)ProductStatus.Removed };
            Update(query, parameters);
        }
    }
}
