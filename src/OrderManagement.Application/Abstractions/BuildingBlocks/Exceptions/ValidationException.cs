using OrderManagement.Application.Abstractions.BuildingBlocks.Validation;

namespace OrderManagement.Application.Abstractions.BuildingBlocks.Exceptions;

public sealed class ValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(IEnumerable<ValidationError> errors)
        : base("One or more validation errors occurred.")
        => Errors = errors
            .GroupBy(e => e.Field)
            .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

    public ValidationException(string field, string message)
        : this([new ValidationError(field, message)])
    {
    }
}