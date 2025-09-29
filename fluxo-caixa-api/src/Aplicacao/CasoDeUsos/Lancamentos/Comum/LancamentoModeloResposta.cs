using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao.CasoDeUsos.Lancamentos.Comum;

public record LancamentoModeloResposta(Guid Id, string Descricao, decimal Valor, ETipoLancamento Tipo, DateTimeOffset DataCriacao)
{
    public static LancamentoModeloResposta CriarDeLancamento(Lancamento lancamento)
        => new(
            lancamento.Id,
            lancamento.Descricao,
            lancamento.Valor,
            lancamento.Tipo,
            lancamento.DataCriacao
            ); 
}
