# Corporate Hotel Booking

Kata got from https://www.codurance.com/katas/corporate-hotel-booking.

Developed with dotnet (c#) and Visual Studio Code.

# Practice objectives

- TDD
- Evolutionary design
- GitHub Copilot

Another important thing that I tried to do is to avoid writing end-to-end tests for all the acceptance criteria that was included in the description of the exercise. That is, I added at least one integrated test that makes sure that all the pieces work together properly, but, if I felt that all the scenarios where already cover by the integrated test and the unit tests, I didn't add additional integrated or end-to-end tests.

Finally I end up adding more than one integrated test in some cases that I didn't feel so confident just with the existing unit tests, but you may notice that the tests located in the project that holds the integrated tests by no means cover all the acceptante test scenarios.

# Introduction

Build a corporate hotel booking engine. This engine has to satisfy the needs of 3 different types of **actors**:

- **Hotel Manager**: Set all the different types of rooms and respective quantities for her hotel.
- **Company Admin**: Add/delete employees and also create booking policies for her company and employees.
- **Employee**: Book a hotel room

To achieve that, the engine needs to provide 4 main services that work in close collaboration with each other.

The four services are described below. The <?> indicates you can use whatever primitive or type you want.

# Hotel Service

Used by the hotel manager to define the types and number of rooms of each type the hotel has. It also can return hotel information given a hotel ID.

```
    public class HotelService {
    
        // Collaborators(?)
    
        void addHotel(<?> hotelId, <?> hotelName);
    
        void setRoom(<?> hotelId, <?> number, <?> roomType);
            
        <?> findHotelBy(<?> hotelId); 
    
    }
```

## Rules

- The addHotel(...) method should throw an exception when the hotel ID already exists or create the hotel otherwise.

- The setRoom(...) method should throw an exception if the hotel does not exist. It should insert or update a room according to its room number.

- The findHotelBy(<?> hotelId) should return all the information about the number of rooms for the specified ID.

# Company Service

Enables company admins to add and delete employees.

```
    public class CompanyService {
                
        // Collaborators(?)
    
        void addEmployee(<?> companyId, <?> employeeId);
        
        void deleteEmployee(<?> employeeId);
    
    }
```

## Rules

- Employees should not be duplicated.
- When deleting an employee, all the bookings and policies associated to the employee should also be deleted from the system.

# Booking Policy Service

Allows company admins to create booking policies for their company and employees. Booking policies determine if an employee booking request is allowed by their company. There are two types of booking policy:

- Company Booking Policy: Indicates which type of rooms can be booked. E.g. a company may only allow standard (single/double) rooms to be booked. Or it may allow standard and junior suite rooms.
- Employee Booking Policy: Indicates which type of rooms a specific employee can book. E.g. One employee might only be allowed to book a standard room while another employee may be allowed to book standard, junior suite and master suite.

```
    public class BookingPolicyService {
    
        // Collaborators(?)
    
        void setCompanyPolicy(<?> companyId, <?> roomTypes);
        
        void setEmployeePolicy(<?> employeeId, <?> roomTypes);
        
        boolean isBookingAllowed(<?> employeeId, <?> roomType);
    
    }
```

## Business Rules

- Employee policies take precedence over company policies. If there is a policy for an employee, the policy should be respected regardless of what the company policy (if any) says.
- If an employee policy does not exist, the company policy should be checked.
- If neither employee nor company policies exist, the employee should be allowed to book any room.

## Technical Rules

- Methods setCompanyPolicy(...) and setEmployeePolicy(...) should create a new policy or update an existing one. No duplicate company or employee policies are allowed.
- Method isBookingAllowed(...) should take into account the employee and the company the employee works for.

# Booking Service

Allows employees to book rooms at hotels.

```
    public class BookingService {
    
        // Collaborators (?)
        
        Booking book(<?> employeeId, <?> hotelId, <?> roomType, Date checkIn, Date checkOut);
    
    }
```

## Rules

- Booking should contain a unique ID, employeeId, hotelId, roomType, checkIn and checkOut.
- Check out date must be at least one day after the check in date.
- Validate if the hotel exists and room type is provided by the hotel
- Verify if booking is allowed according to the booking policies defined, if any. See Booking Policy Service for more details.
- Booking should only be allowed if there is at least one room type available during the whole booking period.
- Keep track of all bookings. E.g. If hotel has 5 standard rooms, we should have no more than 5 bookings in the same day.
- Hotel rooms can be booked many times as long as there are no conflicts with the dates.
- Return booking confirmation to the employee or error otherwise (exceptions can also be used).