namespace OrderManagement.Application.Abstractions.BuildingBlocks.Messaging;

public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default);
}