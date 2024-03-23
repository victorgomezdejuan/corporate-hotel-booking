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

public class Result<T>
{
    public bool IsFailure { get; }
    public string Error { get; }
    public T? Value { get; }

    public Result(bool isFailure, string error, T? value)
    {
        IsFailure = isFailure;
        Error = error;
        Value = value;
    }

    public static Result<T> Success(T value) => new(false, string.Empty, value);
    public static Result<T> Failure(string error) => new(true, error, default);
}