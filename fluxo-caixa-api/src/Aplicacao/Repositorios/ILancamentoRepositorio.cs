using Aplicacao.CasoDeUsos.Lancamentos.Listar;
using Dominio.Comum;
using Dominio.Entidades;

namespace Aplicacao.Repositorios;

public interface ILancamentoRepositorio
    : IRepositorio<Lancamento>,
      IRepositorioLitaFiltrada<Lancamento, FiltroLancamento>
{
}
