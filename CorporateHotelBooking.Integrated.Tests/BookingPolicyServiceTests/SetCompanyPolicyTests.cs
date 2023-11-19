using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using FluentAssertions;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetCompanyPolicyTests
{
    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        var bookingPolicyService = new BookingPolicyService(companyPolicyRepository);
        var companyPolicyToBeAdded = new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite  });

        // Act
        bookingPolicyService.SetCompanyPolicy(companyPolicyToBeAdded.CompanyId, companyPolicyToBeAdded.AllowedRoomTypes.ToList());

        // Assert
        var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(100);
        retrievedCompanyPolicy.Should().Be(companyPolicyToBeAdded);
    }
}