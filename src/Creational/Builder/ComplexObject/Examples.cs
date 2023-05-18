using FluentAssertions;
using Xunit;

namespace Creational.Builder.ComplexObject;

public class Examples
{
    [Fact]
    public void Builder_AllowsToBuildObjectsWithLogicalSeparationOfProps()
    {
        User user = new UserBuilder("some", "guy")
            .Lives
                .At("5th avenue")
                .In("New York City")
                .WithZipCode("EJ2201")
            .Works
                .In("Sales")
                .AsA("Sales Manager")
                .Earning(900_000);

        user.Should().BeEquivalentTo(new User
        {
            FirstName = "some",
            LastName = "guy",
            StreetAdress = "5th avenue",
            City = "New York City",
            Zipcode = "EJ2201",
            Division = "Sales",
            Position = "Sales Manager",
            TotalCompensation = 900_000
        });
    }
}