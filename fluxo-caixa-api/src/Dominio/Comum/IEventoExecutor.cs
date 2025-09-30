
namespace Dominio.Comum;

public interface IEventoExecutor<in T>
    where T : IEvento
{
    Task Executar(T evento, CancellationToken cancellationToken);
}