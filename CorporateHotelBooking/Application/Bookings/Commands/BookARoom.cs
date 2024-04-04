using CorporateHotelBooking.Application.Common;
using CorporateHotelBooking.Application.Common.Mappings;
using CorporateHotelBooking.Domain.Entities;
using CorporateHotelBooking.Domain.Entities.BookingPolicies;
using CorporateHotelBooking.Domain.Exceptions;
using CorporateHotelBooking.Domain.ValueObjects;
using CorporateHotelBooking.Repositories.Bookings;
using CorporateHotelBooking.Repositories.CompanyBookingPolicies;
using CorporateHotelBooking.Repositories.EmployeeBookingPolicies;
using CorporateHotelBooking.Repositories.Employees;
using CorporateHotelBooking.Repositories.Hotels;
using CorporateHotelBooking.Repositories.Rooms;

namespace CorporateHotelBooking.Application.Bookings.Commands;

public record BookARoomCommand
(
    int EmployeeId,
    int HotelId,
    RoomType RoomType,
    DateOnly CheckInDate,
    DateOnly CheckOutDate
);

public class BookARoomCommandHandler
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeBookingPolicyRepository _employeeBookingPolicyRepository;
    private readonly ICompanyBookingPolicyRepository _companyBookingPolicyRepository;

    public BookARoomCommandHandler(
        IBookingRepository bookingRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IEmployeeRepository employeeRepository,
        IEmployeeBookingPolicyRepository employeeBookingPolicyRepository,
        ICompanyBookingPolicyRepository companyBookingPolicyRepository)
    {
        _bookingRepository = bookingRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
        _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
        _companyBookingPolicyRepository = companyBookingPolicyRepository;
    }

    public Result<NewBooking> Handle(BookARoomCommand command)
    {
        var bookingGenerationResult = GetBookingFromCommand(command);
        if (bookingGenerationResult.IsFailure)
        {
            return Result<NewBooking>.Failure(bookingGenerationResult.Error);
        }

        var commandValidationResult = ValidateCommand(command);
        if (commandValidationResult.IsFailure)
        {
            return Result<NewBooking>.Failure(commandValidationResult.Error);
        }

        var createdBooking = _bookingRepository.Add(bookingGenerationResult.Value!);

        return Result<NewBooking>.Success(createdBooking.AsNewBooking());
    }

    private Result ValidateCommand(BookARoomCommand command)
    {
        if (!_hotelRepository.Exists(command.HotelId))
        {
            return Result.Failure("Hotel not found.");
        }

        if (!_employeeRepository.Exists(command.EmployeeId))
        {
            return Result.Failure("Employee not found.");
        }

        bool roomTypeProvided = IsRoomTypeProvidedByTheHotel(command);
        if (!roomTypeProvided)
        {
            return Result.Failure("Room type not provided by the hotel.");
        }

        bool allowed = IsBookingAllowed(command);
        if (!allowed)
        {
            return Result.Failure("Booking not allowed.");
        }

        bool roomsAvailable = AreThereRoomsAvailable(command);
        if (!roomsAvailable)
        {
            return Result.Failure("No rooms of that type available for the requested period.");
        }

        return Result.Success();
    }

    private static Result<Booking> GetBookingFromCommand(BookARoomCommand command)
    {
        try
        {
            var booking = new Booking
            (
                employeeId: command.EmployeeId,
                hotelId: command.HotelId,
                roomType: command.RoomType,
                dateRange: new BookingDateRange(command.CheckInDate, command.CheckOutDate)
            );

            return Result<Booking>.Success(booking);
        }
        catch (CheckOutDateMustBeAfterCheckInDateException)
        {
            return Result<Booking>.Failure("Check-out date must be after check-in date.");
        }
    }

    private bool IsRoomTypeProvidedByTheHotel(BookARoomCommand command)
    {
        if (!_roomRepository.ExistsRoomType(command.HotelId, command.RoomType))
        {
            return false;
        }

        return true;
    }

    private bool IsBookingAllowed(BookARoomCommand command)
    {
        BookingPolicy employeeBookingPolicy = GetEmployeeBookingPolicy(command.EmployeeId);
        BookingPolicy companyBookingPolicy = GetCompanyBookingPolicy(command);

        var bookingPolicy = new AggregatedBookingPolicy(employeeBookingPolicy, companyBookingPolicy);
        if (!bookingPolicy.BookingAllowed(command.RoomType))
        {
            return false;
        }

        return true;
    }

    private BookingPolicy GetEmployeeBookingPolicy(int employeeId)
    {
        BookingPolicy employeeBookingPolicy;
        if (_employeeBookingPolicyRepository.Exists(employeeId))
        {
            employeeBookingPolicy = _employeeBookingPolicyRepository.Get(employeeId)!;
        }
        else
        {
            employeeBookingPolicy = new NonApplicableBookingPolicy();
        }

        return employeeBookingPolicy;
    }

    private BookingPolicy GetCompanyBookingPolicy(BookARoomCommand command)
    {
        BookingPolicy companyBookingPolicy;
        var employee = _employeeRepository.Get(command.EmployeeId);
        if (_companyBookingPolicyRepository.Exists(employee!.CompanyId))
        {
            companyBookingPolicy = _companyBookingPolicyRepository.Get(employee.CompanyId);
        }
        else
        {
            companyBookingPolicy = new NonApplicableBookingPolicy();
        }

        return companyBookingPolicy;
    }

    private bool AreThereRoomsAvailable(BookARoomCommand command)
    {
        var dateRange = new BookingDateRange(command.CheckInDate, command.CheckOutDate);

        if (
            _bookingRepository.GetCount(command.HotelId, command.RoomType, dateRange)
            >=
            _roomRepository.GetCount(command.HotelId, command.RoomType))
        {
            return false;
        }

        return true;
    }
}