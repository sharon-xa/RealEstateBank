namespace RealEstateBank.Utils.Exceptions;

public class DatabaseException : Exception {
    public int StatusCode { get; }
    public string ErrorCode { get; }

    public DatabaseException(
        string message,
        int statusCode = StatusCodes.Status500InternalServerError,
        string errorCode = "DATABASE_ERROR"
    ) : base(message) {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
