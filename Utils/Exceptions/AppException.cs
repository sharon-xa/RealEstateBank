namespace RealEstateBank.Utils.Exceptions;

public class AppException : Exception {
    public int StatusCode { get; }
    public string Service { get; }
    public string Method { get; }

    public AppException(
        string message,
        string service,
        string method,
        int statusCode = StatusCodes.Status500InternalServerError
    ) : base(message) {
        StatusCode = statusCode;
        Service = service;
        Method = method;
    }
}