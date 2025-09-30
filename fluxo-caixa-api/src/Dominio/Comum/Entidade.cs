using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Comum;

public abstract class Entidade(Guid? id = null)
{
    public Guid Id { get; } = id ?? Guid.NewGuid();
    public DateTimeOffset DataCriacao { get; } = DateTimeOffset.UtcNow;

    private readonly List<IEvento> _eventos = [];

    [NotMapped]
    public IReadOnlyCollection<IEvento> Eventos => _eventos.AsReadOnly();

    public void AddEvento(IEvento eventos) => _eventos.Add(eventos);

    public void RemoverEvento(IEvento eventos) => _eventos.Remove(eventos);

    public void LimparEventos() => _eventos.Clear();
}
