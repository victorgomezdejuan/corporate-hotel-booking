using CorporateHotelBooking.Application.CompanyPolicies.Commands.SetCompanyPolicy;
using CorporateHotelBooking.Domain;
using CorporateHotelBooking.Repositories.CompanyPolicies;
using Moq;

namespace CorporateHotelBooking.Unit.Tests.Application.CompanyPolicies.Commands;

public class SetCompanyPolicyTests
{
    [Fact]
    public void AddNewCompanyPolicy()
    {
        // Arrange
        var companyPolicyRepositoryMock = new Mock<ICompanyPolicyRepository>();
        var setCompanyPolicyCommandHandler = new SetCompanyPolicyCommandHandler(companyPolicyRepositoryMock.Object);
        var setCompanyPolicyCommand = new SetCompanyPolicyCommand(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite });

        // Act
        setCompanyPolicyCommandHandler.Handle(setCompanyPolicyCommand);

        // Assert
        companyPolicyRepositoryMock.Verify(r => r.AddCompanyPolicy(new CompanyPolicy(100, new List<RoomType> { RoomType.Standard, RoomType.JuniorSuite })));
    }
}