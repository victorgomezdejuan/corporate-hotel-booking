namespace CorporateHotelBooking.Application.Common;

public class Result
{
    public bool IsFailure { get; }
    public string Error { get; }

    public Result(bool isFailure, string error)
    {
        IsFailure = isFailure;
        Error = error;
    }

    public static Result Success() => new(false, string.Empty);
    public static Result Failure(string error) => new(true, error);
}