using GroceryAppAPI.Models.DbModels;

namespace GroceryAppAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User Get(int id);
        public User Get(string email);
        public void Update(string conditions, User user);
        public int Add(User user);
        void Update(int id, string passwordHash);
    }
}
