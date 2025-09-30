using Dominio.Enum;

namespace Aplicacao.Eventos.Lancamentos;

public record LancamentoCriadoMensagem(Guid Id, string Descricao, decimal Valor, ETipoLancamento Tipo, DateTimeOffset DataCriacao);