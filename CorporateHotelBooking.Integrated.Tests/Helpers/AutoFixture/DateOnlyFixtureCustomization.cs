using AutoFixture;

namespace CorporateHotelBooking.Integrated.Tests.Helpers.AutoFixture;

public class DateOnlyFixtureCustomization: ICustomization
{
    void ICustomization.Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
    }
}