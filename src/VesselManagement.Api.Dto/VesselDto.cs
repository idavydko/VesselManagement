using VesselManagement.DomainModel;

namespace VesselManagement.Api.Dto;

public class VesselDto(Vessel vessel)
{
    public Guid Id { get; set; } = vessel.Id;

    public string Name { get; set; } = vessel.Name!;

    public string IMO { get; set; } = vessel.IMO!;

    public string Type { get; set; } = vessel.Type.ToString();

    public decimal Capacity { get; set; } = vessel.Capacity;
}
