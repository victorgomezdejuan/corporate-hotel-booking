namespace CorporateHotelBooking.Domain;

public class Employee
{
    public Employee(int id, int companyId)
    {
        Id = id;
        CompanyId = companyId;
    }

    public int Id { get; }
    public int CompanyId { get; }

    public override bool Equals(object? obj)
    {
        return obj is Employee employee &&
               Id == employee.Id &&
               CompanyId == employee.CompanyId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CompanyId);
    }
}