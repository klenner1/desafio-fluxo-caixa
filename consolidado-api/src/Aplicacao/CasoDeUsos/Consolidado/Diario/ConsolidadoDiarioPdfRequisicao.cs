using Aplicacao.Interfaces;

namespace Aplicacao.CasoDeUsos.Consolidado.Diario;

public record ConsolidadoDiarioPdfRequisicao(DateOnly Data)
    : IRequisicao<ConsolidadoDiarioPdfResposta>;
