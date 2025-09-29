using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Interfaces;

namespace Aplicacao.CasoDeUsos.Lancamentos.Criar;

public interface ICriarLancamento
    : IUseCase<CriarLancamentoRequisicao, LancamentoModeloResposta>
{ }
