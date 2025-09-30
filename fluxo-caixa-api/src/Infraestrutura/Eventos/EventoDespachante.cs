using Dominio.Comum;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura.Eventos;

public class EventoDespachante(IServiceProvider serviceProvider) : IEventoDespachante
{

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task Despachar(IEvento evento)
    {
        var eventType = evento.GetType();
        var handlerType = typeof(IEventoExecutor<>).MakeGenericType(eventType);

        var handlers = _serviceProvider.GetServices(handlerType);

        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var handleMethod = handlerType.GetMethod(nameof(IEventoExecutor<IEvento>.Executar));

            if (handleMethod != null)
            {
                await (Task)handleMethod.Invoke(handler, [evento, CancellationToken.None])!;
            }
        }
    }

}
