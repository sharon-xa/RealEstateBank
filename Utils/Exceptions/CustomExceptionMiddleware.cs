using Microsoft.IdentityModel.Tokens;

namespace RealEstateBank.Utils.Exceptions;

public class CustomExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception) {
        var response = new ErrorResponse();

        switch (exception) {
            case DatabaseException dbEx:
                httpContext.Response.StatusCode = dbEx.StatusCode;
                response.StatusCode = dbEx.StatusCode;
                response.Message = dbEx.Message;

                _logger.LogError("❌ Database error in {Repository}.{Method} | SQL State: {SqlState} | Message: {Message}",
                    dbEx.Repository, dbEx.Method, dbEx.SqlState, dbEx.Message);
                break;

            case AppException appEx:
                httpContext.Response.StatusCode = appEx.StatusCode;
                response.StatusCode = appEx.StatusCode;
                response.Message = appEx.Message;

                _logger.LogWarning("❌ Business error in {Service}.{Method} | Message: {Message}",
                    appEx.Service,
                    appEx.Method,
                    appEx.Message
                );
                break;

            case SecurityTokenExpiredException tokenEx:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Token expired";

                _logger.LogWarning("⚠️ JWT expired at {ValidTo}, current time {Now}",
                    tokenEx.Expires, DateTime.UtcNow);
                break;

            case SecurityTokenValidationException tokenEx:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Invalid token";

                _logger.LogWarning("⚠️ JWT validation failed: {Message}", tokenEx.Message);
                break;

            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An unexpected error occurred. Please try again later.";

                _logger.LogError(exception, "❌ Unhandled exception: {Message}", exception.Message);
                break;
        }

        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response);
    }
}
