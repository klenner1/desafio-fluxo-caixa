using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Entidades;

namespace Aplicacao.CasoDeUsos.Lancamentos.Criar;

public class CriarLancamento(ILancamentoRepositorio repositorio, IUnidadeDeTrabalho unidadeDeTrabalho) : ICriarLancamento
{
    private readonly ILancamentoRepositorio _repositorio = repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho = unidadeDeTrabalho;

    public async Task<LancamentoModeloResposta> Executar(CriarLancamentoRequisicao requisicao, CancellationToken cancellationToken)
    {
        var lancamento = Lancamento.Criar(
            requisicao.Descricao,
            requisicao.Valor,
            requisicao.Tipo);

        await _repositorio.Inserir(lancamento, cancellationToken);
        await _unidadeDeTrabalho.Commit(cancellationToken);

        return LancamentoModeloResposta.CriarDeLancamento(lancamento);
    }
}
