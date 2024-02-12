using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Helpers;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;

namespace GroceryAppAPI.Services
{
    // Service for managing user orders
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        // Constructor with dependency injection for repositories, services, and context accessor
        public OrderService(IOrderRepository orderRepository, IUserService userService, IPaymentService paymentService, IProductRepository productRepository, IOrderProductRepository orderProductRepository, IHttpContextAccessor contextAccessor)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _paymentService = paymentService;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
            _contextAccessor = contextAccessor;
        }

        // Get all orders for a specific user
        public IEnumerable<OrderResponse> GetAll(int userId)
        {
            var user = _userService.Get(userId);
            if (user is null) { throw new InvalidRequestDataException("UserId is either not given or invalid."); }
            IdentityClaimHelper.ClaimUser(user.Email, _contextAccessor);

            // Retrieve all orders for the user
            var orders = _orderRepository.GetAll(userId);
            var orderResponses = new List<OrderResponse>();

            // Map each order to its response format
            foreach (var order in orders)
            {
                var orderProducts = _orderProductRepository.GetAll(order.Id);
                var productIds = orderProducts.Select(op => op.ProductId);
                orderResponses.Add(new OrderResponse() { OrderId = order.Id, ProductIds = productIds });
            }
            return orderResponses;
        }

        // Place a new order and associated payment
        public OrderPlacementResponse Place(int userId, OrderPlacementRequest paymentRequest)
        {
            if (paymentRequest is null) { throw new ArgumentNullException("Payment request is either not given or invalid."); }
            if (paymentRequest.PaymentRequest is null) { throw new InvalidRequestDataException("Payment details are either not given or invalid."); }
            if (paymentRequest.OrderRequest is null) { throw new InvalidRequestDataException("Order details are either not given or invalid."); }

            // Add the order and retrieve its ID
            var orderId = AddOrder(userId, paymentRequest.OrderRequest);
            int paymentId = 0;

            try
            {
                // Validate payment amount against the total amount of purchased items
                var totalAmount = paymentRequest.OrderRequest.ProductIds.Select(id => _productRepository.Get(id)).Sum(p => p.Price);
                if (totalAmount != paymentRequest.PaymentRequest.Amount) { throw new InvalidRequestDataException("Payment amount is less than the total amount of the purchased items."); }

                // Add the payment and retrieve its ID
                paymentId = _paymentService.Add(paymentRequest.PaymentRequest);
            }
            catch (Exception ex)
            {
                // Rollback if payment fails, delete associated order and order products
                _orderProductRepository.Delete(orderId);
                _orderRepository.Delete(orderId);
                throw new PaymentFailedException(ex.Message);
            }

            // Update the order with the payment ID
            if (paymentId > 0) { _orderRepository.Update(orderId, paymentId); }

            // Return the order placement response
            return new OrderPlacementResponse() { OrderId = orderId, PaymentId = paymentId };
        }

        // Add a new order to the database
        private int AddOrder(int userId, OrderRequest orderRequest)
        {
            if (orderRequest is null) { throw new ArgumentNullException("Order is either null or invalid."); }

            // Validate the order request
            Validate(orderRequest);

            // Create a new order and retrieve its ID
            var order = new Order() { UserId = userId };
            var id = _orderRepository.Add(order);

            // Add each product in the order to the OrderProducts table
            foreach (var productId in orderRequest.ProductIds)
            {
                var orderProduct = new OrderProduct() { OrderId = id, ProductId = productId };
                _orderProductRepository.Add(orderProduct);
            }
            return id;
        }

        // Validate the order request by checking if each product exists
        private void Validate(OrderRequest orderRequest)
        {
            if (orderRequest is null) { throw new ArgumentNullException("Order is either not given or invalid"); }

            // Validate the presence and existence of product IDs
            if (orderRequest.ProductIds != null && orderRequest.ProductIds.Any())
            {
                foreach (var productId in orderRequest.ProductIds)
                {
                    var product = _productRepository.Get(productId);
                    if (product is null) { throw new EntityNotFoundException(productId, "Product"); }
                }
            }
            else { throw new InvalidRequestDataException("ProductIds are either not given or invalid."); }
        }
    }
}
