using Aplicacao.Interfaces;
using Dominio.Enum;

namespace Aplicacao.CasoDeUsos.Lancamentos.Processar;

public record ProcessarLancamentoRequisicao(
    Guid Id, string Descricao, decimal Valor, ETipoLancamento Tipo, DateTimeOffset DataCriacao) : IRequisicao;
