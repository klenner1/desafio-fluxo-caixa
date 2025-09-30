using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Comum;
using Dominio.Entidades;
using Dominio.Eventos;

namespace Aplicacao.CasoDeUsos.Lancamentos.Criar;

public class CriarLancamento(
    ILancamentoRepositorio repositorio,
    IUnidadeDeTrabalho unidadeDeTrabalho,
    IEventoDespachante despachante
    ) : ICriarLancamento
{
    private readonly ILancamentoRepositorio _repositorio = repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho = unidadeDeTrabalho;
    private readonly IEventoDespachante _despachante = despachante;

    public async Task<LancamentoModeloResposta> Executar(CriarLancamentoRequisicao requisicao, CancellationToken cancellationToken)
    {
        var lancamento = Lancamento.Criar(
            requisicao.Descricao,
            requisicao.Valor,
            requisicao.Tipo);

        await _repositorio.Inserir(lancamento, cancellationToken);
        await _unidadeDeTrabalho.Commit(cancellationToken);

        var evento = new LancamentoCriadoEvento(lancamento);
        await _despachante.Despachar(evento);

        return LancamentoModeloResposta.CriarDeLancamento(lancamento);
    }
}
