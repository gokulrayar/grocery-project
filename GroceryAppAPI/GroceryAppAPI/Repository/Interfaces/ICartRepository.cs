using GroceryAppAPI.Models.DbModels;

namespace GroceryAppAPI.Repository.Interfaces
{
    public interface ICartRepository
    {
        public Cart GetByUser(int userId);
        public Cart Get(int id);
        public int Add(Cart cart);
        public void Delete(int id);
    }
}
