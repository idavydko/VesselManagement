using Microsoft.EntityFrameworkCore;
using VesselManagement.DomainModel;
using VesselManagement.DomainModel.Services.DataAccess;

namespace VesselManagement.DataAccess.Repositories;

public class VesselRepository(AppDbContext dbContext) : IVesselRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Vessel>> Get()
    {
        return await _dbContext.Vessels.ToListAsync();
    }

    public async Task<Vessel?> Get(Guid id)
    {
        return await _dbContext.Vessels.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vessel?> Get(string imo)
    {
        return await _dbContext.Vessels.FirstOrDefaultAsync(v => v.IMO == imo);
    }

    public void Add(Vessel vessel)
    {
        _dbContext.Vessels.Add(vessel);
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
 }
