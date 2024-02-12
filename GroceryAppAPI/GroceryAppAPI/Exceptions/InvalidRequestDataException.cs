namespace GroceryAppAPI.Exceptions
{
    // Represents an error which occurs when an invalid request data is given.
    public class InvalidRequestDataException : Exception
    {
        public InvalidRequestDataException(string message)
            : base(message) 
        {
        }
    }
}
