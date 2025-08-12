using Microsoft.AspNetCore.Diagnostics;

namespace RealEstateBank.Utils;

public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler {
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var response = new ErrorResponse() {
            StatusCode = StatusCodes.Status500InternalServerError,
            ExceptionMessage = exception.Message,
            Title = "Something went wrong",
        };

        // TODO: Create a logger and log errors properly.
        logger.LogError("");

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return true;
    }
}
