using GroceryAppAPI.Models.DbModels;

namespace GroceryAppAPI.Repository.Interfaces
{
    public interface ICartProductRepository
    {
        public IEnumerable<CartProduct> GetAll(int cartId);
        public void Add(CartProduct cartProduct);
        public void Delete(int cartId, int productId);
        public void Delete(int cartId);
    }
}
