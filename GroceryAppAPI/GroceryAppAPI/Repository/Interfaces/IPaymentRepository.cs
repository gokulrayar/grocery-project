using GroceryAppAPI.Models.DbModels;

namespace GroceryAppAPI.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        public Payment Get(int id);
        public int Add(Payment payment);
    }
}
