namespace OrderManagement.Application.Abstractions.BuildingBlocks.Results;

public sealed record Error(string Code, string Message, ErrorType Type)
{
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error Failure(string code, string message)  => new(code, message, ErrorType.Failure);
}

public enum ErrorType { Failure, NotFound, Conflict, Forbidden }
