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

    [Fact]
    public void UpdateExistingCompanyPolicy()
    {
        // // Arrange
        // var companyPolicyRepository = new InMemoryCompanyPolicyRepository();
        // var bookingPolicyService = new BookingPolicyService(companyPolicyRepository);
        // var addedPolicy = new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });
        // bookingPolicyService.SetCompanyPolicy(addedPolicy.CompanyId, addedPolicy.AllowedRoomTypes.ToList());
        // var updatedPolicy = new CompanyPolicy(100, new List<RoomType> { RoomType.JuniorSuite, RoomType.MasterSuite });

        // // Act
        // bookingPolicyService.SetCompanyPolicy(updatedPolicy.CompanyId, updatedPolicy.AllowedRoomTypes.ToList());

        // // Assert
        // var retrievedCompanyPolicy = companyPolicyRepository.GetCompanyPolicy(100);
        // retrievedCompanyPolicy.Should().Be(updatedPolicy);
    }
}