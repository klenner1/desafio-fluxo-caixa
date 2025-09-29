using Aplicacao.Comum;
using Infraestrutura.Contextos;
using Microsoft.Extensions.Logging;

namespace Infraestrutura;

public class UnidadeDeTrabalho(
    FluxoCaixaDbContext context,
    ILogger<UnidadeDeTrabalho> logger) : IUnidadeDeTrabalho
{
    private readonly FluxoCaixaDbContext _ctx = context;
    private readonly ILogger<UnidadeDeTrabalho> _logger = logger;

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _ctx.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Informações salvas no repositório com sucesso");
    }

}
