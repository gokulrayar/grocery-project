using GroceryAppAPI.Enumerations;

namespace GroceryAppAPI.Models.Response
{
    // Represents the login response.
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string? AccessToken { get; set; }
        public Role Role { get; set; }
    }
}
