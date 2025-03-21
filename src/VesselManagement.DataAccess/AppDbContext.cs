using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VesselManagement.DomainModel;

namespace VesselManagement.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vessel> Vessels { get; private set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
