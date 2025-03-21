using Bogus;
using VesselManagement.DomainModel;

namespace VesselManagement.UnitTests.DomainModel;

[TestClass]
public sealed class CreateVesselTests
{
    [TestMethod]
    public void Success_Test()
    {
        var faker = new Faker();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var actual = new Vessel(expectedName, expectedIMO, expectedType, expectedCapacity);

        Assert.AreEqual(expectedName, actual.Name);
        Assert.AreEqual(expectedIMO, actual.IMO);
        Assert.AreEqual(expectedType, actual.Type);
        Assert.AreEqual(expectedCapacity, actual.Capacity);
    }
}
