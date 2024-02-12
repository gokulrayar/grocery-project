namespace GroceryAppAPI.Models.Request
{
    // Represents a login request.
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
