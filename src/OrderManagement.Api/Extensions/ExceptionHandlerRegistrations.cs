using OrderManagement.Api.ExceptionHandlers;

namespace OrderManagement.Api.Extensions;

internal static class ExceptionHandlerRegistrations
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}