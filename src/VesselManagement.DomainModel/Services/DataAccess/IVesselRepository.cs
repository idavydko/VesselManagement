namespace VesselManagement.DomainModel.Services.DataAccess;

public interface IVesselRepository
{
    Task<List<Vessel>> Get();
    Task<Vessel?> Get(Guid id);
    Task<Vessel?> Get(string imo);
    void Add(Vessel vessel);
    Task SaveChanges();
}
