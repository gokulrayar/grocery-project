using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services;
using Moq;
using Newtonsoft.Json;

namespace GroceryAppAPITests.Mocks
{
    public static class CartMock
    {
        private static string BasePath = Environment.CurrentDirectory + "/TestData/";
        public static Mock<ICartRepository> CartRepositoryMock = new Mock<ICartRepository>();
        public static Mock<ICartProductRepository> CartProductRepositoryMock = new Mock<ICartProductRepository>();
        public static Mock<IProductRepository> ProductRepositoryMock = new Mock<IProductRepository>();
        public static Mock<IUserRepository> UserRepositoryMock = new Mock<IUserRepository>();
        public static void SetMocks()
        {
            MockCartRepository();
            MockCartProductRepository();
            MockProductRepository();
            MockUserRepository();
        }
        private static void MockCartRepository()
        {
            CartRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var fileContent = File.ReadAllText(BasePath + "carts.json");
                var carts = JsonConvert.DeserializeObject<IEnumerable<Cart>>(fileContent);
                return carts.FirstOrDefault(c => c.Id == id);
            });

            CartRepositoryMock.Setup(repo => repo.GetByUser(It.IsAny<int>())).Returns((int userId) =>
            {
                var fileContent = File.ReadAllText(BasePath + "carts.json");
                var carts = JsonConvert.DeserializeObject<IEnumerable<Cart>>(fileContent);
                return carts.FirstOrDefault(c => c.UserId == userId);
            });

            CartRepositoryMock.Setup(repo => repo.Add(It.IsAny<Cart>())).Returns(3);
            CartRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>()));
        }
        private static void MockCartProductRepository()
        {
            CartProductRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>())).Returns((int cartId) =>
            {
                var fileContent = File.ReadAllText(BasePath + "carts_products.json");
                var cartProducts = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(fileContent);
                return cartProducts.Where(cp => cp.CartId == cartId);
            });

            CartProductRepositoryMock.Setup(repo => repo.Add(It.IsAny<CartProduct>()));
            CartProductRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>()));
        }
        private static void MockProductRepository()
        {
            ProductRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var fileContent = File.ReadAllText(BasePath + "products.json");
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(fileContent);
                return products.FirstOrDefault(p => p.Id == id);
            });
        }
        private static void MockUserRepository()
        {
            UserRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var fileContent = File.ReadAllText(BasePath + "users.json");
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(fileContent);
                return users.FirstOrDefault(user => user.Id == id);
            });
        }
    }
}
