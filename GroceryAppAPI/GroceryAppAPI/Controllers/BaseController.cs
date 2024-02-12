using GroceryAppAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // BaseController class that serves as the base for other controllers in the application
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class BaseController : ControllerBase
    {
        // This class is intended to be used as a base for other controllers.
        // It includes the CommonExceptionFilterAttribute, which is a custom exception filter
        // designed to catch common exceptions and set the HTTP response accordingly.

        // The CommonExceptionFilterAttribute is applied globally to all actions in controllers derived from this base,
        // providing a centralized mechanism for handling and responding to common exceptions.

        // No additional code is present in this class, as its primary purpose is to provide a common base for controllers
        // and apply the CommonExceptionFilterAttribute.
    }
}
