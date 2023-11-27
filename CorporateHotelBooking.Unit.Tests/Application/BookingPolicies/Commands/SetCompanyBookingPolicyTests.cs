using CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Commands;

public class SetCompanyBookingPolicyTests
{
    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepositoryMock = new Mock<ICompanyBookingPolicyRepository>();
        var setCompanyPolicyCommandHandler = new SetCompanyBookingPolicyCommandHandler(companyPolicyRepositoryMock.Object);
        var setCompanyPolicyCommand = new SetCompanyBookingPolicyCommand(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        companyPolicyRepositoryMock.Verify(r => r.AddCompanyPolicy(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepositoryMock = new Mock<ICompanyBookingPolicyRepository>();
        var setCompanyPolicyCommandHandler = new SetCompanyBookingPolicyCommandHandler(companyPolicyRepositoryMock.Object);
        companyPolicyRepositoryMock.Setup(r => r.Exists(100)).Returns(true);
        var setCompanyPolicyCommand = new SetCompanyBookingPolicyCommand(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        companyPolicyRepositoryMock.Verify(r => r.UpdateCompanyPolicy(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}