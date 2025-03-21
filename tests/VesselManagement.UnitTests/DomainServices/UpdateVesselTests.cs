using Bogus;
using Moq;
using System.Net;
using VesselManagement.DomainModel;
using VesselManagement.DomainModel.Services.DataAccess;
using VesselManagement.DomainServices.Commands;

namespace VesselManagement.UnitTests.DomainServices;

[TestClass]
public sealed class UpdateVesselTests
{
    private readonly Mock<IVesselRepository> _vesselRepository;
    private UpdateVesselHandler _createVesselHandler;

    public UpdateVesselTests()
    {
        _vesselRepository = new Mock<IVesselRepository>();
    }

    [TestMethod]
    public async Task Success_Test()
    {
        var faker = new Faker();
        var expectedId = Guid.NewGuid();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var vessel = new Vessel(
            faker.Name.FirstName(),
            faker.Random.Number(1, int.MaxValue).ToString(),
            VesselType.Cargo,
            faker.Random.Number(1, int.MaxValue));

        typeof(Vessel).GetProperty(nameof(Vessel.Id))?.SetValue(vessel, expectedId);

        _vesselRepository
            .Setup(x => x.Get(expectedId))
            .ReturnsAsync(vessel);

        var request = 
            new UpdateVessel(expectedId, expectedName, expectedIMO, expectedType, expectedCapacity);

        _createVesselHandler = new UpdateVesselHandler(_vesselRepository.Object);

        var response = await _createVesselHandler.Handle(request, CancellationToken.None);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        Assert.AreEqual(expectedId, vessel.Id);
        Assert.AreEqual(expectedName, vessel.Name);
        Assert.AreEqual(expectedIMO, vessel.IMO);
        Assert.AreEqual(expectedType, vessel.Type);
        Assert.AreEqual(expectedCapacity, vessel.Capacity);
    }


    [TestMethod]
    public async Task Fail_NotFound_Test()
    {
        var wrongId = Guid.NewGuid();

        var faker = new Faker();
        var expectedId = Guid.NewGuid();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var vessel = new Vessel(
            faker.Name.FirstName(),
            faker.Random.Number(1, int.MaxValue).ToString(),
            VesselType.Cargo,
            faker.Random.Number(1, int.MaxValue));

        typeof(Vessel).GetProperty(nameof(Vessel.Id))?.SetValue(vessel, expectedId);

        _vesselRepository
            .Setup(x => x.Get(expectedId))
            .ReturnsAsync(vessel);

        var request =
            new UpdateVessel(wrongId, expectedName, expectedIMO, expectedType, expectedCapacity);

        _createVesselHandler = new UpdateVesselHandler(_vesselRepository.Object);

        var response = await _createVesselHandler.Handle(request, CancellationToken.None);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        Assert.AreEqual(expectedId, vessel.Id);
        Assert.AreNotEqual(expectedName, vessel.Name);
        Assert.AreNotEqual(expectedIMO, vessel.IMO);
        Assert.AreNotEqual(expectedType, vessel.Type);
        Assert.AreNotEqual(expectedCapacity, vessel.Capacity);
    }

    [TestMethod]
    public async Task Fail_Conflict_Test()
    {
        var faker = new Faker();
        var expectedId = Guid.NewGuid();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var vessel = new Vessel(
            faker.Name.FirstName(),
            faker.Random.Number(1, int.MaxValue).ToString(),
            VesselType.Cargo,
            faker.Random.Number(1, int.MaxValue));

        var vesselWithSameIMO = new Vessel(
            faker.Name.FirstName(),
            expectedIMO,
            VesselType.Cargo,
            faker.Random.Number(1, int.MaxValue));

        typeof(Vessel).GetProperty(nameof(Vessel.Id))?.SetValue(vessel, expectedId);

        _vesselRepository
            .Setup(x => x.Get(expectedId))
            .ReturnsAsync(vessel);

        _vesselRepository
            .Setup(x => x.Get(expectedIMO))
            .ReturnsAsync(vesselWithSameIMO);

        var request =
            new UpdateVessel(expectedId, expectedName, expectedIMO, expectedType, expectedCapacity);

        _createVesselHandler = new UpdateVesselHandler(_vesselRepository.Object);

        var response = await _createVesselHandler.Handle(request, CancellationToken.None);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

        Assert.AreEqual(expectedId, vessel.Id);
        Assert.AreNotEqual(expectedName, vessel.Name);
        Assert.AreNotEqual(expectedIMO, vessel.IMO);
        Assert.AreNotEqual(expectedType, vessel.Type);
        Assert.AreNotEqual(expectedCapacity, vessel.Capacity);
    }
}
