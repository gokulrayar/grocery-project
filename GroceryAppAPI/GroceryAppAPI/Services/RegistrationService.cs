using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Helpers;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;

namespace GroceryAppAPI.Services
{
    // Service for user registration
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;

        // Constructor with dependency injection for IUserRepository
        public RegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Register a new user based on the provided registration request
        public void Register(RegistrationRequest registrationRequest)
        {
            // Validate the registration request
            Validate(registrationRequest);

            // Hash the password before storing it in the database
            registrationRequest.Password = EncodingHelper.HashPassword(registrationRequest.Password);

            // Create a new user object and add it to the repository
            var user = new User()
            {
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                Email = registrationRequest.Email,
                Password = registrationRequest.Password,
                Address = registrationRequest.Address,
                Gender = (int)registrationRequest.Gender,
                Role = (int)registrationRequest.Role
            };

            _userRepository.Add(user);
        }

        // Validate the registration request for required fields and constraints
        private void Validate(RegistrationRequest registrationRequest)
        {
            // Check if the registration request is null
            if (registrationRequest is null) { throw new ArgumentNullException("User is either not given or invalid."); }

            // Check if required fields are present and valid
            if (string.IsNullOrWhiteSpace(registrationRequest.FirstName)) { throw new InvalidRequestDataException("FirstName is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(registrationRequest.LastName)) { throw new InvalidRequestDataException("LastName is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(registrationRequest.Email)) { throw new InvalidRequestDataException("Email is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(registrationRequest.Password)) { throw new InvalidRequestDataException("Password is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(registrationRequest.Address)) { throw new InvalidRequestDataException("Address is either not given or invalid."); }
            if (!Enum.IsDefined(typeof(Gender), registrationRequest.Gender)) { throw new InvalidRequestDataException("Gender is either not given or invalid."); }
            if (!Enum.IsDefined(typeof(Role), registrationRequest.Role)) { throw new InvalidRequestDataException("Role is either not given or invalid."); }

            // Check if an existing user is already registered with the same email
            var existingUser = _userRepository.Get(registrationRequest.Email);
            if (existingUser != null) { throw new InvalidRequestException("An user is already registered with the same email."); }
        }
    }
}
