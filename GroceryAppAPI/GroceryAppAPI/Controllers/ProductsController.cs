using GroceryAppAPI.Attributes;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for managing products
    [Route("[controller]")]
    [ApiController]
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor to inject the product service dependency
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Endpoint to add a new product (requires "Admin" role)
        [Authorize(Roles = "Admin")] // Requires authorization with the "Admin" role
        [HttpPost]
        public IActionResult Post([FromBody] ProductRequest product)
        {
            // Call the product service to add a new product
            var id = _productService.Add(product);

            // Return Ok result with the newly created product's ID
            return Ok(new { data = new { Id = id } });
        }

        // Endpoint to get all products (requires authorization)
        [Authorize] // Requires authorization
        [HttpGet]
        public IActionResult GetAll()
        {
            // Call the product service to get all products
            var products = _productService.GetAll();

            // Return Ok result with the list of products
            return Ok(new { data = products });
        }

        // Endpoint to update specific properties of a product (requires "Admin" role)
        [Authorize(Roles = "Admin")] // Requires authorization with the "Admin" role
        [HttpPatch("{id:int}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] string properties)
        {
            // Call the product service to update specific properties of the product
            _productService.Update(id, properties);

            // Return Ok result with a success message
            return Ok(new { Message = "Product updated successfully." });
        }

        // Endpoint to delete a product (requires "Admin" role)
        [Authorize(Roles = "Admin")] // Requires authorization with the "Admin" role
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // Call the product service to delete the product
            _productService.Delete(id);

            // Return Ok result with a success message
            return Ok(new { Message = "Product deleted successfully." });
        }
    }
}
