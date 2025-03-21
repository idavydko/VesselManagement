using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VesselManagement.DomainModel;

namespace VesselManagement.DataAccess.ValueConverters;

internal class VesselTypeConverter : ValueConverter<VesselType, string>
{
    public static IReadOnlyDictionary<string, VesselType> TypeCodeToType { get; } =
        new Dictionary<string, VesselType>
        {
                { "Cargo", VesselType.Cargo },
                { "Tanker", VesselType.Tanker },
                { "Passenger", VesselType.Passenger }
        };

    public static IReadOnlyDictionary<VesselType, string> TypeToTypeCode { get; } =
        TypeCodeToType.ToDictionary(keyValuePair => keyValuePair.Value, keyValuePair => keyValuePair.Key);

    public VesselTypeConverter()
        : base(to => TypeToTypeCode[to], from => TypeCodeToType[from])
    {
    }
}
