using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Application.Abstractions.BuildingBlocks.Messaging;

public sealed class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    // request type -> cached wrapper (avoids reflection on every call)
    private static readonly ConcurrentDictionary<Type, object> Wrappers = new();

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var wrapper = (HandlerWrapper<TResponse>)Wrappers.GetOrAdd(request.GetType(), type =>
            Activator.CreateInstance(
                typeof(HandlerWrapperImpl<,>).MakeGenericType(type, typeof(TResponse)))!);

        return wrapper.HandleAsync(request, serviceProvider, ct);
    }

    private abstract class HandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider sp, CancellationToken ct);
    }

    private sealed class HandlerWrapperImpl<TRequest, TResponse> : HandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider sp, CancellationToken ct)
            => sp.GetRequiredService<IHandler<TRequest, TResponse>>().HandleAsync((TRequest)request, ct);
    }
}