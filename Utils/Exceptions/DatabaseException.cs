namespace RealEstateBank.Utils.Exceptions;

public class DatabaseException : Exception {
    public int StatusCode { get; }
    public string? SqlState { get; }
    public string Repository { get; set; }
    public string Method { get; set; }

    public DatabaseException(
        string message,
        string repository,
        string method,
        int statusCode = StatusCodes.Status500InternalServerError,
        string sqlState = "DATABASE_ERROR"
    ) : base(message) {
        StatusCode = statusCode;
        SqlState = sqlState;
        Repository = repository;
        Method = method;
    }
}
