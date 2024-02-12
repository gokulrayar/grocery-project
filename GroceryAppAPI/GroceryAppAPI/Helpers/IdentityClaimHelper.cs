using GroceryAppAPI.Exceptions;
using System.Security.Claims;

namespace GroceryAppAPI.Helpers
{
    // This helper class provides methods for handling identity claims.
    public static class IdentityClaimHelper
    {
        public static bool ClaimUser(string email, IHttpContextAccessor contextAccessor)
        {
            // Retrieve the claims associated with the current user identity.
            var identity = contextAccessor.HttpContext.User.Claims;

            // Find the claim with the specified email.
            var identityClaim = identity.FirstOrDefault(id => id.Type == ClaimTypes.Email && id.Value == email);

            // If the claim is not found, throw an exception indicating denial of access.
            if (identityClaim is null)
            {
                throw new InvalidRequestException("User is denied access to the specified resource.");
            }

            // User successfully claimed.
            return true;
        }
    }
}
