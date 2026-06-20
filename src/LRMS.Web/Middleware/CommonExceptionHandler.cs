using LRMS.Application.Exceptions;
using LRMS.Infrastructure.Exceptions;
using LRMS.Web.Contracts;
using Microsoft.AspNetCore.Diagnostics;

namespace LRMS.Web.Middleware;

internal class CommonExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            DomainException => StatusCodes.Status409Conflict,
            DataValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse(exception.Message), cancellationToken);

        return true;
    }
}
