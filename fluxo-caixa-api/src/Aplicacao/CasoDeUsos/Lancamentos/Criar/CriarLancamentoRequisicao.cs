using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Interfaces;
using Dominio.Enum;

namespace Aplicacao.CasoDeUsos.Lancamentos.Criar;

public record CriarLancamentoRequisicao(
    string Descricao, decimal Valor, ETipoLancamento Tipo) : IRequisicao<LancamentoModeloResposta>;
