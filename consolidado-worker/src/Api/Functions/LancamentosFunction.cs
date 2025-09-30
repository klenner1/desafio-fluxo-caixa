using Aplicacao.CasoDeUsos.Lancamentos.Processar;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Api.Functions;


public class LancamentosController(ILogger<LancamentosController> logger, IProcessarLancamento casoDeUso)
{
    private readonly ILogger<LancamentosController> _logger = logger;
    private readonly IProcessarLancamento _casoDeUso = casoDeUso;

    [Function("ProcessarLancamentos")]
    public async Task Run(
            [RabbitMQTrigger("consolidado-worker.lancamento.criado", ConnectionStringSetting = "RabbitMQConnection")]
            string mensagem,
            //FunctionContext context,
            CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicio Processamento");

        var lancamento = JsonSerializer.Deserialize<ProcessarLancamentoRequisicao>(mensagem)!;

        await _casoDeUso.Executar(lancamento, cancellationToken);
    }
}
