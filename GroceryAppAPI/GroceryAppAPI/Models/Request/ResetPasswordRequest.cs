namespace GroceryAppAPI.Models.Request
{
    // Represents a reset password request.
    public class ResetPasswordRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
