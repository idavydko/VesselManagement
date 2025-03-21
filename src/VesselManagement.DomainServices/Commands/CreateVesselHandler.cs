using MediatR;
using System.Net;
using VesselManagement.DomainModel;
using VesselManagement.DomainModel.Services.DataAccess;

namespace VesselManagement.DomainServices.Commands;

public class CreateVesselHandler(IVesselRepository vesselRepository) 
    : IRequestHandler<CreateVessel, CreateVesselResponse>
{
    private readonly IVesselRepository _vesselRepository = vesselRepository;

    public async Task<CreateVesselResponse> Handle(CreateVessel request, CancellationToken cancellationToken)
    {
        var existedVesselIMO = await _vesselRepository.Get(request.IMO);
        if (existedVesselIMO is not null)
        {
            return new CreateVesselResponse(HttpStatusCode.Conflict);
        }

        var vessel = new Vessel(request.Name, request.IMO, request.Type, request.Capacity);
        _vesselRepository.Add(vessel);

        await _vesselRepository.SaveChanges();

        return new CreateVesselResponse(HttpStatusCode.OK, vessel.Id);
    }
}
