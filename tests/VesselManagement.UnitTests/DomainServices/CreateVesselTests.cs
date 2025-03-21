using Bogus;
using Moq;
using System.Net;
using VesselManagement.DomainModel;
using VesselManagement.DomainModel.Services.DataAccess;
using VesselManagement.DomainServices.Commands;

namespace VesselManagement.UnitTests.DomainServices;

[TestClass]
public sealed class CreateVesselTests
{
    private readonly Mock<IVesselRepository> _vesselRepository;
    private CreateVesselHandler _createVesselHandler;

    public CreateVesselTests()
    {
        _vesselRepository = new Mock<IVesselRepository>();
    }

    [TestMethod]
    public async Task Success_Test()
    {
        var faker = new Faker();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var request = new CreateVessel(expectedName, expectedIMO, expectedType, expectedCapacity);

        _createVesselHandler = new CreateVesselHandler(_vesselRepository.Object);

        var response = await _createVesselHandler.Handle(request, CancellationToken.None);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task Fail_Conflict_Test()
    {
        var faker = new Faker();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        _vesselRepository
            .Setup(x => x.Get(It.IsAny<string>()))
            .ReturnsAsync(
                new Vessel(
                    faker.Name.FirstName(),
                    expectedIMO,
                    VesselType.Cargo,
                    faker.Random.Number(1, int.MaxValue)));

        var request = new CreateVessel(expectedName, expectedIMO, expectedType, expectedCapacity);

        _createVesselHandler = new CreateVesselHandler(_vesselRepository.Object);

        var response = await _createVesselHandler.Handle(request, CancellationToken.None);

        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
    }
}
