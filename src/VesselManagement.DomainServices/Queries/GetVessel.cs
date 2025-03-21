using MediatR;
namespace VesselManagement.DomainServices.Queries;

public class GetVessel(Guid id)
    : IRequest<GetVesselResponse>
{
    public Guid Id { get; } = id;
}
