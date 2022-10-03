using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BurberDiner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        //using proleme details
        var problemDetails = new ProblemDetails{
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1",
            Title = "A error occured while processing your request",
            Status = (int)HttpStatusCode.InternalServerError,
        };
        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}