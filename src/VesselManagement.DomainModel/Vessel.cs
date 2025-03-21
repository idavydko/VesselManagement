namespace VesselManagement.DomainModel;

public class Vessel
{
    public Guid Id { get; private set; }

    public string? Name { get; private set; }

    // International Maritime Organization number
    public string? IMO { get; private set; }

    public VesselType Type { get; private set; }

    public decimal Capacity { get; private set; }

    private Vessel()
    { }

    public Vessel(string name, string imo, VesselType type, decimal capacity)
        : this()
    {
        Name = name;
        IMO = imo;
        Type = type;
        Capacity = capacity;
    }

    public void Update(string name, string imo, VesselType type, decimal capacity)
    {
        Name = name;
        IMO = imo;
        Type = type;
        Capacity = capacity;
    }
}
