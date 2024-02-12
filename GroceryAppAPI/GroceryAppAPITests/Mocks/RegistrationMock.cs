using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services;
using Moq;
using Newtonsoft.Json;

namespace GroceryAppAPITests.Mocks
{
    /// <summary>
    /// Mocks the repositories used by <see cref="RegistrationService"/>.
    /// </summary>
    public static class RegistrationMock
    {
        private static string BasePath = Environment.CurrentDirectory + "/TestData/";
        public static Mock<IUserRepository> UserRepositoryMock = new Mock<IUserRepository>();

        /// <summary>
        /// Sets the mocks.
        /// </summary>
        public static void SetMocks()
        {
            MockUserRepository();
        }

        /// <summary>
        /// Mocks the implementation of <see cref="IUserRepository"/>.
        /// </summary>
        private static void MockUserRepository()
        {
            UserRepositoryMock.Setup(repo => repo.Get(It.IsAny<string>())).Returns((string email) =>
            {
                var fileContent = File.ReadAllText(BasePath + "users.json");
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(fileContent);
                return users.FirstOrDefault(user => user.Email == email);
            });
        }
    }
}
