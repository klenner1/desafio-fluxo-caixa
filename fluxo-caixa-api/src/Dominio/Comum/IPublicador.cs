namespace Dominio.Comum;

public interface IPublicador
{
    Task Publicar<T>(string topico, T evento, CancellationToken cancellationToken);
}
