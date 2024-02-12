using GroceryAppAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GroceryAppAPI.Attributes
{
    // A filter that catches all the common exceptions and sets the HTTP response accordingly.
    public class CommonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // Overrides the OnException method to handle exceptions.
        public override void OnException(ExceptionContext context)
        {
            // Check if the provided context is null, and throw an ArgumentNullException if true.
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Switch statement to handle different types of exceptions.
            switch (context.Exception)
            {
                // If the exception is of type InvalidRequestException, InvalidRequestDataException,
                // PaymentFailedException, ArgumentNullException, or EntityNotFoundException.
                case InvalidRequestException:
                case InvalidRequestDataException:
                case PaymentFailedException:
                case ArgumentNullException:
                case EntityNotFoundException:
                default:
                    // Set the HTTP response to a BadRequestObjectResult with a message from the exception.
                    context.Result = new BadRequestObjectResult(new { Message = context.Exception.Message });
                    break;
            }
        }
    }
}