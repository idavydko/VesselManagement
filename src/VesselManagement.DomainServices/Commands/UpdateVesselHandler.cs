using MediatR;
using System.Net;
using VesselManagement.DomainModel.Services.DataAccess;

namespace VesselManagement.DomainServices.Commands;

public class UpdateVesselHandler(IVesselRepository vesselRepository)
    : IRequestHandler<UpdateVessel, UpdateVesselResponse>
{
    private readonly IVesselRepository _vesselRepository = vesselRepository;

    public async Task<UpdateVesselResponse> Handle(UpdateVessel request, CancellationToken cancellationToken)
    {
        var vessel = await _vesselRepository.Get(request.Id);

        if (vessel is null)
        {
            return new UpdateVesselResponse(HttpStatusCode.NotFound);
        }

        if (vessel.IMO != request.IMO)
        {
            var vesselWithDuplicatedIMO = await _vesselRepository.Get(request.IMO);
            if (vesselWithDuplicatedIMO is not null)
            {
                return new UpdateVesselResponse(HttpStatusCode.Conflict);
            }
        }

        vessel.Update(request.Name, request.IMO, request.Type, request.Capacity);

        await _vesselRepository.SaveChanges();

        return new UpdateVesselResponse(HttpStatusCode.OK);
    }
}
