using System.ComponentModel.DataAnnotations;
using VesselManagement.DomainModel;

namespace VesselManagement.Api.Dto;

public class VesselModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? IMO { get; set; }

    [Required]
    public VesselType? Type { get; set; }
    
    [Required]
    public decimal? Capacity { get; set; }
}
