using Dominio.Comum;
using Dominio.Entidades;

namespace Dominio.Eventos;

public class LancamentoCriadoEvento(Lancamento lancamento) : IEvento
{
    public Lancamento Lancamento { get; private set; } = lancamento;
}
