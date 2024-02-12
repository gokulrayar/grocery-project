using GroceryAppAPI.Models.DbModels;
namespace GroceryAppAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAll(int userId);
        public int Add(Order order);
        public void Update(int id, int paymentId);
        public void Delete(int id);
    }
}
