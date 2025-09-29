using Aplicacao.Geradores;
using Aplicacao.Repositorios;

namespace Aplicacao.CasoDeUsos.Consolidado.Diario;

public class ConsolidadoDiarioPdf(ILancamentoRepositorio repositorio, ISaldoDiarioConsolidadoGeradorPdf geradorPdf) : IConsolidadoDiarioPdf
{
    private readonly ILancamentoRepositorio _repositorio = repositorio;
    private readonly ISaldoDiarioConsolidadoGeradorPdf _geradorPdf = geradorPdf;

    public async Task<ConsolidadoDiarioPdfResposta> Executar(ConsolidadoDiarioPdfRequisicao requisicao, CancellationToken cancellationToken)
    {
        var lancamentos = await _repositorio.ListarDiario(requisicao.Data, cancellationToken);
        var arquivoBytes = _geradorPdf.Gerar(lancamentos);

        return new(arquivoBytes);
    }
}
