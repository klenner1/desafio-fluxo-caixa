namespace Dominio.Comum;

public abstract class Entidade(Guid? id = null)
{
    public Guid Id { get; set; } = id ?? Guid.NewGuid();
    public DateTimeOffset DataCriacao { get; set; } = DateTimeOffset.UtcNow;
}
