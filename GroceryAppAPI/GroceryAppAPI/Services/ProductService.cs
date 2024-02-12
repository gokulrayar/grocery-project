using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace GroceryAppAPI.Services
{
    // Service for managing products
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Constructor with dependency injection for IProductRepository
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Add a new product to the database
        public int Add(ProductRequest productRequest)
        {
            // Validate the product request
            Validate(productRequest);

            // Create a new product object and add it to the repository
            var product = new Product()
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Stock = productRequest.Stock,
                ImageUrl = productRequest.ImageUrl,
                Status = (int)productRequest.Status
            };

            return _productRepository.Add(product);
        }

        // Delete a product by updating its status as removed
        public void Delete(int id)
        {
            _productRepository.UpdateStatusAsRemoved(id);
        }

        // Get all existing products with their details
        public IEnumerable<ProductResponse> GetAll()
        {
            var productResponses = new List<ProductResponse>();

            // Retrieve all products from the repository
            var products = _productRepository.GetAll();

            // Map each product to its response format
            foreach (var product in products)
            {
                productResponses.Add(new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl,
                    Status = (ProductStatus)product.Status
                });
            }

            // Filter and return only products with existing status
            return productResponses.Where(p => p.Status is ProductStatus.Existing);
        }

        // Update a product based on provided properties
        public void Update(int id, string properties)
        {
            var existingProduct = _productRepository.Get(id);

            // Throw exception if the product does not exist
            if (existingProduct is null) { throw new EntityNotFoundException(id, "Product"); }

            if (!string.IsNullOrWhiteSpace(properties))
            {
                var jsonProperties = JObject.Parse(properties);
                var setStatements = new List<string>();
                var product = new Product() { Id = id };

                foreach (var propertyInfo in jsonProperties.Properties())
                {
                    var name = propertyInfo.Name;
                    var value = propertyInfo.Value.ToString();

                    // Switch case to handle different properties
                    switch (name.ToUpperInvariant())
                    {
                        case "NAME":
                            if (string.IsNullOrWhiteSpace(value)) { throw new InvalidRequestDataException("Name is either not given or invalid."); }
                            setStatements.Add("[Name] = @Name");
                            product.Name = value;
                            break;
                        case "PRICE":
                            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var price) && price > 0)
                            {
                                setStatements.Add("[Price] = @Price");
                                product.Price = price;
                            }
                            else { throw new InvalidRequestDataException("Price is either not given or invalid."); }
                            break;
                        case "STOCK":
                            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var stock) && stock >= 0)
                            {
                                setStatements.Add("[Stock] = @Stock");
                                product.Stock = stock;
                            }
                            else { throw new InvalidRequestDataException("Stock is either not given or invalid."); }
                            break;
                        case "IMAGEURL":
                            if (string.IsNullOrEmpty(value)) { throw new InvalidRequestDataException("ImageUrl is either not given or invalid."); }
                            setStatements.Add("[ImageUrl] = @ImageUrl");
                            product.ImageUrl = value;
                            break;
                        case "STATUS":
                            if (!Enum.IsDefined(typeof(ProductStatus), value)) { throw new InvalidRequestDataException("ProductStatus is either not given or invalid."); }
                            setStatements.Add("[Status] = @Status");
                            product.Status = int.Parse(value);
                            break;
                        default:
                            throw new InvalidRequestDataException($"Product does not have any property like '{name}'");
                    }
                }

                // Concatenate set statements and update the product in the repository
                var query = string.Join(", ", setStatements);
                _productRepository.Update(query, product);
            }
        }

        // Validate the product request
        private void Validate(ProductRequest productRequest)
        {
            // Check if the product request is null
            if (productRequest is null) { throw new ArgumentNullException("Product is either null or invalid."); }

            // Check if required fields are present and valid
            if (string.IsNullOrWhiteSpace(productRequest.Name)) { throw new InvalidRequestDataException("Name is either not given or invalid."); }
            if (productRequest.Price <= 0) { throw new InvalidRequestDataException("Price is either not given or invalid."); }
            if (productRequest.Stock <= 0) { throw new InvalidRequestDataException("Stock is either not given or invalid."); }
            if (string.IsNullOrEmpty(productRequest.ImageUrl)) { throw new InvalidRequestDataException("ImageUrl is either not given or invalid."); }
            if (!Enum.IsDefined(typeof(ProductStatus), productRequest.Status)) { throw new InvalidRequestDataException("ProductStatus is either not given or invalid."); }
        }
    }
}
