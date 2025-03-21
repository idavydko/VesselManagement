using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VesselManagement.DataAccess.ValueConverters;
using VesselManagement.DomainModel;

namespace VesselManagement.DataAccess.Configurations;

public class VesselConfiguration : IEntityTypeConfiguration<Vessel>
{
    public void Configure(EntityTypeBuilder<Vessel> builder)
    {
        builder
            .ToTable(nameof(Vessel));

        builder
            .HasKey(v => v.Id);

        builder
            .Property(v => v.Name)
            .HasColumnType("NVARCHAR (256)")
            .IsRequired();

        builder
            .Property(v => v.IMO)
            .HasColumnType("NVARCHAR (256)")
            .IsRequired();

        builder
            .Property(v => v.Type)
            .IsRequired();

        builder
            .Property(v => v.Capacity)
            .HasColumnType("DECIMAL (19, 4)")
            .IsRequired();

        builder
            .HasIndex(v => v.IMO)
            .IsUnique();

        builder
            .Property(nameof(Vessel.Type))
            .HasConversion(new VesselTypeConverter());
    }
}
