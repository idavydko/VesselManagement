using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VesselManagement.Api.Dto;
using VesselManagement.DomainServices.Commands;
using VesselManagement.DomainServices.Queries;

namespace VesselManagement.Api.Controllers;

/// <summary>
/// CRUD operations with vessels
/// </summary>
[Route("api/vessels")]
[ApiController]
public class VesselsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Retrieve a list of all vessels
    /// </summary>
    /// <returns>list of all vessels</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VesselDto>))]
    public async Task<List<VesselDto>> Get()
    {
        var query = new GetAllVessels();

        var response = await _mediator.Send(query);

        return [.. response.Vessels.Select(v => new VesselDto(v))];
    }

    /// <summary>
    /// Retrieve a specific vessel by id
    /// </summary>
    /// <param name="id">vessel id</param>
    /// <returns>specific vessel</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VesselDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<VesselDto?> Get(Guid id)
    {
        var query = new GetVessel(id);

        var response = await _mediator.Send(query);

        return response.Vessel != null ? new VesselDto(response.Vessel) : null;
    }

    /// <summary>
    /// Register a new vessel
    /// </summary>
    /// <param name="model">vessel details</param>
    /// <returns>new vessel id</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VesselModel model)
    {
        var request = new CreateVessel(model.Name!, model.IMO!, model.Type!.Value, model.Capacity!.Value);

        var response = await _mediator.Send(request);

        return response.StatusCode == HttpStatusCode.OK
            ? StatusCode((int)response.StatusCode, response.Id)
            : StatusCode((int)response.StatusCode);
    }

    /// <summary>
    /// Update an existing vessel
    /// </summary>
    /// <param name="id">vessel id</param>
    /// <param name="model">vessel details</param>
    /// <returns>operation status</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] VesselModel model)
    {
        var request = 
            new UpdateVessel(id, model.Name!, model.IMO!, model.Type!.Value, model.Capacity!.Value);

        var response = await _mediator.Send(request);

        return StatusCode((int)response.StatusCode);
    }
}
