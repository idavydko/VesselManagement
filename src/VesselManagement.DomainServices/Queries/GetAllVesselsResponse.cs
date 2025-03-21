using VesselManagement.DomainModel;

namespace VesselManagement.DomainServices.Queries;

public class GetAllVesselsResponse(List<Vessel> vessels)
{
    public List<Vessel> Vessels { get; set; } = vessels;
}
