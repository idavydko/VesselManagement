using System.Net;

namespace VesselManagement.DomainServices.Commands;

public class UpdateVesselResponse(HttpStatusCode statusCode)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}
