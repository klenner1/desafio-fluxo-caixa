using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Entidades;

namespace Aplicacao.CasoDeUsos.Lancamentos.Processar;

public class ProcessarLancamento(ILancamentoRepositorio repositorio, IUnidadeDeTrabalho unidadeDeTrabalho)
    : IProcessarLancamento
{
    private readonly ILancamentoRepositorio _repositorio = repositorio;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho = unidadeDeTrabalho;

    public async Task Executar(ProcessarLancamentoRequisicao requisicao, CancellationToken cancellationToken)
    {
        if (await _repositorio.Existe(requisicao.Id, cancellationToken))
            return;

        var lancamento = new Lancamento
        {
            Id = requisicao.Id,
            Descricao = requisicao.Descricao,
            Tipo = requisicao.Tipo,
            Valor = requisicao.Valor,
            DataCriacao = requisicao.DataCriacao
        };

        await _repositorio.Inserir(lancamento, cancellationToken);
        await _unidadeDeTrabalho.Commit(cancellationToken);
    }
}
