using Dominio.Comum;
using Dominio.Entidades;

namespace Aplicacao.Repositorios;

public interface ILancamentoRepositorio
    : IRepositorio
{
    Task<IEnumerable<Lancamento>> ListarDiario(DateOnly data, CancellationToken cancellationToken);
}
