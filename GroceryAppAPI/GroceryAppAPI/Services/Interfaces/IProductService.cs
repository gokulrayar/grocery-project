using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using System.Collections.Generic;

namespace GroceryAppAPI.Services.Interfaces
{
    // Interface for a product service
    public interface IProductService
    {
        // Method to get all products
        IEnumerable<ProductResponse> GetAll();

        // Method to add a new product and return the product ID
        int Add(ProductRequest productRequest);

        // Method to update a product by ID based on specified properties
        void Update(int id, string properties);

        // Method to delete a product by ID
        void Delete(int id);
    }
}
