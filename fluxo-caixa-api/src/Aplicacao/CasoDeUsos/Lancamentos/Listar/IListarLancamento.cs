using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Interfaces;

namespace Aplicacao.CasoDeUsos.Lancamentos.Listar;

public interface IListarLancamento
    : IUseCase<ListarLancamentoRequisicao,IEnumerable<LancamentoModeloResposta>>
{ }
