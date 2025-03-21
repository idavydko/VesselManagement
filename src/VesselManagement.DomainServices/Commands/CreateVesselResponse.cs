using System.Net;

namespace VesselManagement.DomainServices.Commands;

public class CreateVesselResponse
{
    public HttpStatusCode StatusCode { get; }

    public Guid Id { get; }

    public CreateVesselResponse(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public CreateVesselResponse(HttpStatusCode statusCode, Guid id)
        : this(statusCode)
    {
        Id = id;
    }
}
