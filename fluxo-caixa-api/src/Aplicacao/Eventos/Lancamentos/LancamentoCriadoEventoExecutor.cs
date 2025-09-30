using Dominio.Comum;
using Dominio.Entidades;
using Dominio.Eventos;

namespace Aplicacao.Eventos.Lancamentos;

public class LancamentoCriadoEventoExecutor(IPublicador publicador)
    : IEventoExecutor<LancamentoCriadoEvento>
{

    private readonly IPublicador _publicador = publicador;

    public async Task Executar(LancamentoCriadoEvento evento, CancellationToken cancellationToken)
    {
        Lancamento lancamento = evento.Lancamento;
        var mensagem = new LancamentoCriadoMensagem(
            lancamento.Id,
            lancamento.Descricao,
            lancamento.Valor,
            lancamento.Tipo,
            lancamento.DataCriacao);

        await _publicador.Publicar("fluxo-caixa.lancamento.criado",mensagem, cancellationToken);
    }
}
