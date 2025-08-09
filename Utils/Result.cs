using System.Net;

namespace RealEstateBank.Utils;

public class Result<T> {
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; } // e.g., 200, 400, 404, 500
    public T? Value { get; set; }
    public string? Error { get; set; }

    public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK) {
        return new Result<T> { IsSuccess = true, StatusCode = statusCode, Value = value };
    }

    public static Result<T> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest) {
        return new Result<T> { IsSuccess = false, StatusCode = statusCode, Error = error };
    }
}
