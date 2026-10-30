namespace OrderManagement.Application.Abstractions.BuildingBlocks.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; }

    public string? ErrorMessage { get; set; }

    protected Result(bool ok, Error? error) { IsSuccess = ok; Error = error; }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
}

public sealed class Result<T> : Result
{
    public T? Value { get; set; }

    private Result(T value) : base(true, null) => Value = value;

    private Result(Error error) : base(false, error)
    {
    }

    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(Error error) => new(error);
}