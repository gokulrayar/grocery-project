using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Helpers;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;

namespace GroceryAppAPI.Services
{
    // Service for managing shopping carts
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartProductRepository _cartProductRepository;
        private readonly IUserService _userService;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        // Constructor with dependency injection for repositories, services, and context accessor
        public CartService(ICartRepository cartRepository, ICartProductRepository cartProductRepository, IUserService userService, IProductRepository productRepository, IHttpContextAccessor contextAccessor)
        {
            _cartRepository = cartRepository;
            _cartProductRepository = cartProductRepository;
            _userService = userService;
            _productRepository = productRepository;
            _contextAccessor = contextAccessor;
        }

        // Add a product to the user's cart
        public int Add(int userId, CartRequest cartRequest)
        {
            if (cartRequest is null) { throw new ArgumentNullException("Cart is either not given or invalid."); }
            var user = _userService.Get(userId);
            IdentityClaimHelper.ClaimUser(user.Email, _contextAccessor);

            // Check if the user already has a cart
            var existingCart = _cartRepository.GetByUser(userId);
            if (existingCart != null) { throw new InvalidRequestException("A cart already exists."); }

            // Retrieve the product and create a new cart
            var product = _productRepository.Get(cartRequest.ProductId);
            if (product is null) { throw new EntityNotFoundException(cartRequest.ProductId, "Product"); }
            var cart = new Cart() { UserId = userId };
            var cartId = _cartRepository.Add(cart);

            // Add the product to the cart
            var cartProduct = new CartProduct() { CartId = cartId, ProductId = cartRequest.ProductId };
            _cartProductRepository.Add(cartProduct);

            return cartId;
        }

        // Get the user's cart details
        public CartResponse Get(int userId)
        {
            var user = _userService.Get(userId);
            var cart = _cartRepository.GetByUser(userId);

            // Return null if the user does not have a cart
            if (cart is null) { return null; }

            // Retrieve cart products and extract product IDs
            var cartProducts = _cartProductRepository.GetAll(cart.Id);
            var productIds = cartProducts.Select(x => x.ProductId);
            return new CartResponse { CartId = cart.Id, ProductIds = productIds };
        }

        // Delete the user's cart
        public void Delete(int id, int userId)
        {
            var user = _userService.Get(userId);
            IdentityClaimHelper.ClaimUser(user.Email, _contextAccessor);

            // Delete cart products and the cart itself
            _cartProductRepository.Delete(id);
            _cartRepository.Delete(id);
        }

        // Update the user's cart based on the provided cart request
        public void Update(int id, int userId, CartRequest cartRequest)
        {
            if (cartRequest is null) { throw new ArgumentNullException("Cart is either null or invalid."); }
            var user = _userService.Get(userId);
            IdentityClaimHelper.ClaimUser(user.Email, _contextAccessor);

            // Retrieve the existing cart
            var existingCart = _cartRepository.Get(id);
            if (existingCart is null) { throw new EntityNotFoundException(id, "Cart"); }

            // Retrieve the product
            var product = _productRepository.Get(cartRequest.ProductId);
            if (product is null) { throw new EntityNotFoundException(cartRequest.ProductId, "Product"); }

            // Check and perform the cart operation based on the operation type
            if (!Enum.IsDefined(typeof(CartOperationType), cartRequest.OperationType)) { throw new InvalidRequestDataException("OperationType is either not given or invalid."); }
            if (cartRequest.OperationType is CartOperationType.Add)
            {
                // Add the product to the cart if not already added
                var existingCartProducts = _cartProductRepository.GetAll(id);
                if (existingCartProducts.Select(x => x.ProductId).Contains(cartRequest.ProductId)) { throw new InvalidRequestDataException("Product is already added to the cart."); }
                var cartProduct = new CartProduct() { CartId = id, ProductId = cartRequest.ProductId };
                _cartProductRepository.Add(cartProduct);
            }
            if (cartRequest.OperationType is CartOperationType.Delete) { _cartProductRepository.Delete(id, cartRequest.ProductId); }

            // Retrieve cart products and delete the cart if empty
            var cartProducts = _cartProductRepository.GetAll(id);
            if (!(cartProducts != null && cartProducts.Any())) { _cartRepository.Delete(id); }
        }
    }
}
