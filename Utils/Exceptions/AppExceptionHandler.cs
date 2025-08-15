using Microsoft.AspNetCore.Diagnostics;

namespace RealEstateBank.Utils.Exceptions;

public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler {
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    ) {
        var response = new ErrorResponse();

        switch (exception) {
            case DatabaseException dbEx:
                httpContext.Response.StatusCode = dbEx.StatusCode;
                response.StatusCode = dbEx.StatusCode;
                response.Message = dbEx.Message;

                logger.LogError(dbEx,
                    "❌ Database error in {Repository}.{Method} | SQL State: {SqlState} | Message: {Message}",
                    dbEx.Repository, dbEx.Method, dbEx.SqlState, dbEx.Message);
                break;
            case AppException appEx:
                httpContext.Response.StatusCode = appEx.StatusCode;
                response.StatusCode = appEx.StatusCode;
                response.Message = appEx.Message;

                logger.LogWarning(appEx,
                    "❌ Business error in {Service}.{Method} | Message: {Message}",
                    appEx.Service, appEx.Method, appEx.Message);
                break;
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = exception.Message;

                logger.LogError(exception, "❌ Unhandled exception: {Message}", exception.Message);
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
