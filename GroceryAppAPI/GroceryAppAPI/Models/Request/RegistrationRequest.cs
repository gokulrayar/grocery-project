using GroceryAppAPI.Enumerations;
namespace GroceryAppAPI.Models.Request
{
    // Represents a registration request.
    public class RegistrationRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }
}
