using GroceryAppAPI.Enumerations;
using GroceryAppAPI.Exceptions;
using GroceryAppAPI.Helpers;
using GroceryAppAPI.Models.DbModels;
using GroceryAppAPI.Models.Request;
using GroceryAppAPI.Models.Response;
using GroceryAppAPI.Repository.Interfaces;
using GroceryAppAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroceryAppAPI.Services
{
    // Service for handling user authentication
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        // Constructor with dependency injection for IUserRepository and IConfiguration
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Method for user login
        public LoginResponse Login(LoginRequest loginRequest)
        {
            // Validate login request
            if (string.IsNullOrWhiteSpace(loginRequest.Email)) { throw new InvalidRequestDataException("Username is either not given or invalid."); }
            if (string.IsNullOrWhiteSpace(loginRequest.Password)) { throw new InvalidRequestDataException("Password is either not given or invalid."); }

            // Retrieve user by email
            var user = _userRepository.Get(loginRequest.Email);
            if (user is null) { throw new EntityNotFoundException("User with the given username not found."); }

            // Validate password
            var actualHash = user.Password;
            var expectedHash = EncodingHelper.HashPassword(loginRequest.Password);
            if (actualHash != expectedHash) { throw new InvalidRequestException("Password is incorrect."); }

            // Generate and return access token
            var accessToken = GenerateAccessToken(user);
            return new LoginResponse()
            {
                UserId = user.Id,
                AccessToken = accessToken,
                Role = (Role)user.Role
            };
        }

        // Method to generate JWT access token
        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Authentication:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userRole = (Role)user.Role;

            // Define claims for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, userRole.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Create and configure the JWT token
            var token = new JwtSecurityToken(_configuration["AppSettings:Authentication:Issuer"],
                _configuration["AppSettings:Authentication:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            // Write and return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
