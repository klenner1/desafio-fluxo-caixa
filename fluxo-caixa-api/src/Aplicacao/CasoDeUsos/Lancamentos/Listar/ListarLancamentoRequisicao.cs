using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Interfaces;
using Dominio.Enum;

namespace Aplicacao.CasoDeUsos.Lancamentos.Listar;

public record ListarLancamentoRequisicao(
    IEnumerable<ETipoLancamento>? Tipos,
    DateTimeOffset? DataCriacaoDe,
    DateTimeOffset? DataCriacaoAte)
    : IRequisicao<IEnumerable<LancamentoModeloResposta>>;
