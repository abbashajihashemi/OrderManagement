namespace OrderManagement.Application.Abstractions.BuildingBlocks.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    protected Result(bool ok, string? errorMessage)
    {
        IsSuccess = ok;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string errorMessage) => new(false, errorMessage);
}

public sealed class Result<T> : Result
{
    public T? Value { get; set; }

    private Result(T value) : base(true, null) => Value = value;

    private Result(string errorMessage) : base(false, errorMessage)
    {
    }

    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(string errorMessage) => new(errorMessage);
}