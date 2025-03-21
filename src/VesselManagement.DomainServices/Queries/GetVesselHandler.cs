using MediatR;
using VesselManagement.DomainModel.Services.DataAccess;

namespace VesselManagement.DomainServices.Queries;

public class GetVesselHandler(IVesselRepository vesselRepository)
    : IRequestHandler<GetVessel, GetVesselResponse>
{
    private readonly IVesselRepository _vesselRepository = vesselRepository;

    public async Task<GetVesselResponse> Handle(GetVessel request, CancellationToken cancellationToken)
    {
        var vessel = await _vesselRepository.Get(request.Id);

        return new GetVesselResponse
        {
            Vessel = vessel
        };
    }
}
