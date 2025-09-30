using Dominio.Comum;
using Dominio.Entidades;

namespace Aplicacao.Repositorios;

public interface ILancamentoRepositorio
    : IRepositorio<Lancamento>
{
    Task<bool> Existe(Guid id, CancellationToken cancellationToken);
}
