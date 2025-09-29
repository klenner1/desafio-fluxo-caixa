using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Repositorios;

namespace Aplicacao.CasoDeUsos.Lancamentos.Listar;

public class ListarLancamento(ILancamentoRepositorio repositorio) : IListarLancamento
{
    private readonly ILancamentoRepositorio _repositorio = repositorio;

    public async Task<IEnumerable<LancamentoModeloResposta>> Executar(ListarLancamentoRequisicao requisicao, CancellationToken cancellationToken)
    {
        var filtro = new FiltroLancamento(
            requisicao.Tipos,
            requisicao.DataCriacaoDe,
            requisicao.DataCriacaoAte);

        var lancamentos = await _repositorio.Listar(filtro, cancellationToken);

        return lancamentos.Select(x => LancamentoModeloResposta.CriarDeLancamento(x));
    }

}
