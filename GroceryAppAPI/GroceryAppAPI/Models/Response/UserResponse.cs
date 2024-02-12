using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Models.DbModels;

namespace GroceryAppAPI.Models.Response
{
    // Represents the User response
    public class UserResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }
}
