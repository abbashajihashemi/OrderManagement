using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.ExceptionHandlers;

internal class NotFoundExceptionHandler(
    IProblemDetailsService problemDetailsService
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception exception, CancellationToken ct)
    {
        if (exception is not NotFoundException ex) return false;

        ctx.Response.StatusCode = StatusCodes.Status404NotFound;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = ctx,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "The requested resource was not found",
                Detail = ex.Message
            }
        });
    }
}