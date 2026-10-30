namespace OrderManagement.Application.Abstractions.BuildingBlocks.Messaging;

public interface IDispatcher
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}