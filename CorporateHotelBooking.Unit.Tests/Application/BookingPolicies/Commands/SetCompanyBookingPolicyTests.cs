using AutoFixture.Xunit2;
using CorporateHotelBooking.Application.BookingPolicies.Commands.SetCompanyBookingPolicy;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Unit.Tests.Helpers.AutoFixture;
using FluentAssertions;
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
        var result = _setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        result.IsFailure.Should().BeFalse();
        _companyPolicyRepositoryMock.Verify(r => r.Add(
            new CompanyBookingPolicy(
                companyId,
                roomTypes)));
    }

    [Theory, AutoData]
    public void UpdateExistingCompanyPolicy(int companyId, [CollectionSize(2)] List<RoomType> roomTypes)
    {
        // Arrange
        _companyPolicyRepositoryMock.Setup(r => r.Exists(companyId)).Returns(true);
        var setCompanyPolicyCommand = new SetCompanyBookingPolicyCommand(companyId, roomTypes);

        // Act
        var result = _setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        result.IsFailure.Should().BeFalse();
        _companyPolicyRepositoryMock.Verify(r => r.Update(new CompanyBookingPolicy(companyId, roomTypes)));
    }
}