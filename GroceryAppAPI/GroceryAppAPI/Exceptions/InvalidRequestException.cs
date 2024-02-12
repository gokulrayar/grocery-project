namespace GroceryAppAPI.Exceptions
{
    // Represents an error which occurs when a service is requested with invalid request data.
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message)
            : base(message)
        {   
        }
    }
}
