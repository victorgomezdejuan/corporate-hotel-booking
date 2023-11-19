using CorporateHotelBooking.Domain;

namespace CorporateHotelBooking.Integrated.Tests.BookingPolicyServiceTests;

public class SetEmployeePolicyTests
{
    [Fact]
    public void AddNewEmployeePolicy()
    {
        // // Arrange
        // var employeePolicyRepository = new InMemoryEmployeePolicyRepository();
        // var bookingPolicyService = new BookingPolicyService(employeePolicyRepository);
        // var employeePolicyToBeAdded = new EmployeePolicy(1, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite  });

        // // Act
        // bookingPolicyService.SetEmployeePolicy(employeePolicyToBeAdded.CompanyId, employeePolicyToBeAdded.AllowedRoomTypes.ToList());

        // // Assert
        // var retrievedEmployeePolicy = employeePolicyRepository.GetEmployeePolicy(1);
        // retrievedEmployeePolicy.Should().Be(employeePolicyToBeAdded);
    }
}