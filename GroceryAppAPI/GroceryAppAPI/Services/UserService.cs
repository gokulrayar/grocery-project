using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Helpers;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace GroceryAppAPI.Services
{
    // Implementation of the IUserService interface for managing user-related operations
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        // Constructor with dependency injection for IUserRepository and IHttpContextAccessor
        public UserService(IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        // Get user details by ID and update identity claims
        public UserResponse Get(int id)
        {
            var user = _userRepository.Get(id);
            IdentityClaimHelper.ClaimUser(user.Email, _contextAccessor);

            if (user is null) { throw new EntityNotFoundException(id, "User"); }

            // Map user details to UserResponse model
            return new UserResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Gender = (Gender)user.Gender,
                Role = (Role)user.Role
            };
        }

        // Update user properties based on JSON string of key-value pairs
        public void Update(int id, string properties)
        {
            // Retrieve user by ID
            Get(id);

            if (!string.IsNullOrWhiteSpace(properties))
            {
                // Parse JSON properties
                var jsonProperties = JObject.Parse(properties);
                var setStatements = new List<string>();
                var user = new User() { Id = id };

                foreach (var property in jsonProperties.Properties())
                {
                    var name = property.Name.ToUpperInvariant();
                    var value = property.Value.ToString();

                    // Update user properties based on JSON key-value pairs
                    switch (name)
                    {
                        case "FIRSTNAME":
                            if (string.IsNullOrWhiteSpace(value)) { throw new InvalidRequestDataException("FirstName is either not given or invalid."); }
                            else { setStatements.Add("[FirstName] = @FirstName"); user.FirstName = value; }
                            break;
                        case "LASTNAME":
                            if (string.IsNullOrWhiteSpace(value)) { throw new InvalidRequestDataException("LastName is either not given or invalid."); }
                            else { setStatements.Add("[LastName] = @LastName"); user.LastName = value; }
                            break;
                        case "ADDRESS":
                            if (string.IsNullOrWhiteSpace(value)) { throw new InvalidRequestDataException("Address is either not given or invalid."); }
                            else { setStatements.Add("[Address] = @Address"); user.Address = value; }
                            break;
                        default:
                            throw new InvalidRequestDataException($"User does not have any property like '{name}'");
                    }
                }

                // Build and execute the update query
                var query = string.Join(", ", setStatements);
                _userRepository.Update(query, user);
            }
        }

        // Update user password based on ResetPasswordRequest
        public void UpdatePassword(ResetPasswordRequest resetPasswordRequest)
        {
            // Validate reset password request
            if (resetPasswordRequest is null) { throw new InvalidRequestDataException("Reset request is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(resetPasswordRequest.Email)) { throw new InvalidRequestDataException("Email is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(resetPasswordRequest.Password)) { throw new InvalidRequestDataException("Password is either not given or invalid."); }

            // Retrieve user by email
            var user = _userRepository.Get(resetPasswordRequest.Email);
            if (user is null) { throw new EntityNotFoundException($"User with the given email '{resetPasswordRequest.Email}' does not exist."); }

            // Hash the new password and update the user's password
            var passwordHash = EncodingHelper.HashPassword(resetPasswordRequest.Password);
            _userRepository.Update(user.Id, passwordHash);
        }
    }
}
