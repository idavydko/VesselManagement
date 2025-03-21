using Bogus;
using VesselManagement.DomainModel;

namespace VesselManagement.UnitTests.DomainModel;

[TestClass]
public sealed class UpdateVesselTests
{
    [TestMethod]
    public void Success_Test()
    {
        var faker = new Faker();
        var expectedName = faker.Name.FirstName();
        var expectedIMO = faker.Random.Number(1, int.MaxValue).ToString();
        var expectedType = VesselType.Tanker;
        var expectedCapacity = faker.Random.Number(1, int.MaxValue);

        var vessel = new Vessel(
            faker.Name.FirstName(), 
            faker.Random.Number(1, int.MaxValue).ToString(),
            VesselType.Cargo,
            faker.Random.Number(1, int.MaxValue));

        Assert.AreNotEqual(expectedName, vessel.Name);
        Assert.AreNotEqual(expectedIMO, vessel.IMO);
        Assert.AreNotEqual(expectedType, vessel.Type);
        Assert.AreNotEqual(expectedCapacity, vessel.Capacity);

        vessel.Update(expectedName, expectedIMO, expectedType, expectedCapacity);

        Assert.AreEqual(expectedName, vessel.Name);
        Assert.AreEqual(expectedIMO, vessel.IMO);
        Assert.AreEqual(expectedType, vessel.Type);
        Assert.AreEqual(expectedCapacity, vessel.Capacity);
    }
}
