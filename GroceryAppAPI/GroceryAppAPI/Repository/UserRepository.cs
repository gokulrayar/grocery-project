using GroceryAppAPI.Configurations;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace GroceryAppAPI.Repository
{
    // Repository for managing operations related to users
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        // Constructor to set the database connection using dependency injection
        public UserRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.DefaultConnection)
        {
        }

        // Method to get a user from the database by ID
        public User Get(int id)
        {
            const string query = @"SELECT *
                                   FROM [Users]
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id };
            return Get(query, parameters);
        }

        // Method to get a user from the database by email
        public User Get(string email)
        {
            const string query = @"SELECT *
                                   FROM [Users]
                                   WHERE [Email] = @Email";
            var parameters = new { Email = email };
            return GetAll(query, parameters).FirstOrDefault();
        }

        // Method to update a user in the database based on specified conditions
        public void Update(string conditions, User user)
        {
            string query = @$"UPDATE [Users]
                              SET {conditions}
                              WHERE [Id] = @Id";
            Update(query, (object)user);
        }

        // Method to update the password of a user in the database by ID
        public void Update(int id, string passwordHash)
        {
            const string query = @"UPDATE [Users]
                                   SET [Password] = @Password
                                   WHERE [Id] = @Id";
            var parameters = new { Id = id, Password = passwordHash };
            Update(query, parameters);
        }

        // Method to add a new user to the database
        public int Add(User user)
        {
            const string query = @"INSERT INTO [Users] ([FirstName], [LastName], [Email], [Password], [Address], [Gender], [Role])
                                   OUTPUT INSERTED.Id
                                   VALUES (@FirstName, @LastName, @Email, @Password, @Address, @Gender, @Role)";
            return Add(query, user);
        }
    }
}
