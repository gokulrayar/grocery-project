using GroceryAppAPI.Models.DbModels;
namespace GroceryAppAPI.Repository.Interfaces
{
    public interface IOrderProductRepository
    {
        public IEnumerable<OrderProduct> GetAll(int orderId);
        public int Add(OrderProduct orderProduct);
        public void Delete(int orderId);
    }
}
