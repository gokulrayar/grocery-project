namespace GroceryAppAPI.Exceptions
{
    // Represents an error which occurs when an entity is not found.
    public class EntityNotFoundException : Exception
    {
        // Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        public EntityNotFoundException(int id, string entityName)
            :base($"{entityName} with id {id} is not found.")
        {
        }
        public EntityNotFoundException(string message)
            :base(message)
        {            
        }
    }
}
