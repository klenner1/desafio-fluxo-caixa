namespace Dominio.Comum;

public abstract class Entidade(Guid? id = null)
{
    public Guid Id { get; } = id ?? Guid.NewGuid();
    public DateTimeOffset DataCriacao { get; } = DateTimeOffset.UtcNow;
}
