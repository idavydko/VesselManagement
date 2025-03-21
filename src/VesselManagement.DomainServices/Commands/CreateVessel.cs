using MediatR;
using VesselManagement.DomainModel;

namespace VesselManagement.DomainServices.Commands;

public class CreateVessel(
    string name, 
    string imo, 
    VesselType type, 
    decimal capacity) 
    : IRequest<CreateVesselResponse>
{
    public string Name { get; } = name;
    public string IMO { get; } = imo;
    public VesselType Type { get; } = type;
    public decimal Capacity { get; } = capacity;
}
