using MediatR;
using VesselManagement.DomainModel.Services.DataAccess;

namespace VesselManagement.DomainServices.Queries;

public class GetAllVesselsHandler(IVesselRepository vesselRepository)
    : IRequestHandler<GetAllVessels, GetAllVesselsResponse>
{
    private readonly IVesselRepository _vesselRepository = vesselRepository;

    public async Task<GetAllVesselsResponse> Handle(GetAllVessels request, CancellationToken cancellationToken)
    {
        var vessels = await _vesselRepository.Get();

        return new GetAllVesselsResponse(vessels);
    }
}
