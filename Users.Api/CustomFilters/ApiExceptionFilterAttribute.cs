using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Users.Api.Exceptions;

namespace Users.Api.CustomFilters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException validationEx:
                HandleValidationException(context, validationEx);
                break;
            case NotFoundException notFoundEx:
                HandleNotFoundException(context, notFoundEx);
                break;            
            default:
                HandleUnknownException(context);
                break;
        }

        base.OnException(context);
    }

    private static void HandleValidationException(ExceptionContext context, ValidationException exception)
    {        
        context.Result = new BadRequestObjectResult(new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        });

        context.ExceptionHandled = true;
    }    

    private static void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
    {        
        context.Result = new NotFoundObjectResult(new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        });

        context.ExceptionHandled = true;
    }    

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
