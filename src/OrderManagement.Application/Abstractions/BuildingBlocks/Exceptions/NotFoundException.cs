namespace OrderManagement.Application.Abstractions.BuildingBlocks.Exceptions;

public sealed class NotFoundException(string resource, object key)
    : Exception($"{resource} with key '{key}' was not found.");