using Dominio.Comum;
using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao.CasoDeUsos.Lancamentos.Listar;

public record FiltroLancamento(
    IEnumerable<ETipoLancamento>? Tipos,
    DateTimeOffset? DataCriacaoDe,
    DateTimeOffset? DataCriacaoAte) : IFiltro<Lancamento>;