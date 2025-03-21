using MediatR;
using VesselManagement.DomainModel;

namespace VesselManagement.DomainServices.Commands;

public class UpdateVessel(
    Guid id,
    string name, 
    string imo, 
    VesselType type, 
    decimal capacity) 
    : IRequest<UpdateVesselResponse>
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string IMO { get; } = imo;
    public VesselType Type { get; } = type;
    public decimal Capacity { get; } = capacity;
}
