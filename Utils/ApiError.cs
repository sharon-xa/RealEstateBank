using System.Net;

namespace RealEstateBank.Utils;

public class ApiError : Exception {
    public string ResponseMessage { get; }
    public HttpStatusCode StatusCode { get; }
    public string? DeveloperMessage { get; }
    public string? SourceService { get; }
    public object? AdditionalData { get; }

    public ApiError(
        string responseMessage,
        HttpStatusCode statusCode,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage) {
        ResponseMessage = responseMessage;
        StatusCode = statusCode;
        DeveloperMessage = developerMessage;
        SourceService = sourceService;
        AdditionalData = additionalData;
    }
}
