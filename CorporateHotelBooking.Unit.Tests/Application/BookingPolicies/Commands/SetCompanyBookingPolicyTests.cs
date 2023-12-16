using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.BookingPolicies.Commands;

public class SetCompanyBookingPolicyTests
{
    private readonly Mock<ICompanyBookingPolicyRepository> _companyPolicyRepositoryMock;
    private readonly SetCompanyBookingPolicyCommandHandler _setCompanyPolicyCommandHandler;

    public SetCompanyBookingPolicyTests()
    {
        _companyPolicyRepositoryMock = new Mock<ICompanyBookingPolicyRepository>();
        _setCompanyPolicyCommandHandler =
            new SetCompanyBookingPolicyCommandHandler(_companyPolicyRepositoryMock.Object);
    }

    [Theory, AutoData]
    public void AddNewCompanyPolicy(int companyId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Arrange
        var setCompanyPolicyCommand = new SetCompanyBookingPolicyCommand(
            companyId,
            roomTypes);

        // Act
        _setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        _companyPolicyRepositoryMock.Verify(r => r.Add(
            new CompanyBookingPolicy(
                companyId,
                roomTypes)));
    }

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // Arrange
        _companyPolicyRepositoryMock.Setup(r => r.Exists(100)).Returns(true);
        var setCompanyPolicyCommand = new SetCompanyBookingPolicyCommand(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        _setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        _companyPolicyRepositoryMock.Verify(r => r.Update(new CompanyBookingPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}