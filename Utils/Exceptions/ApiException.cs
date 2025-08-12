using System.Net;

namespace RealEstateBank.Utils;

public class ApiException : Exception {
    public string ResponseMessage { get; }
    public HttpStatusCode StatusCode { get; }
    public string? DeveloperMessage { get; }
    public string? SourceService { get; }
    public object? AdditionalData { get; }

    public ApiException(
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

public class BadRequest : ApiException {
    public BadRequest(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.BadRequest, developerMessage, sourceService, additionalData) {
    }
}

public class Unauthorized : ApiException {
    public Unauthorized(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.Unauthorized, developerMessage, sourceService, additionalData) {
    }
}

public class Forbidden : ApiException {
    public Forbidden(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.Forbidden, developerMessage, sourceService, additionalData) {
    }
}

public class NotFound : ApiException {
    public NotFound(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.NotFound, developerMessage, sourceService, additionalData) {
    }
}

public class Conflict : ApiException {
    public Conflict(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.Conflict, developerMessage, sourceService, additionalData) {
    }
}

public class Internal : ApiException {
    public Internal(
        string responseMessage,
        string? developerMessage = null,
        string? sourceService = null,
        object? additionalData = null
    ) : base(responseMessage, HttpStatusCode.InternalServerError, developerMessage, sourceService, additionalData) {
    }
}