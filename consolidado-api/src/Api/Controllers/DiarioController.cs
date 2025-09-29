using Aplicacao.CasoDeUsos.Consolidado.Diario;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiarioController() : ControllerBase
{
    [HttpGet("")]
    public async Task<FileResult> Get(
        [FromServices] IConsolidadoDiarioPdf casoDeUso,
        [FromQuery] ConsolidadoDiarioPdfRequisicao requisicao,
        CancellationToken cancellationToken)
    {
        var resposta = await casoDeUso.Executar(requisicao, cancellationToken);

        string contentType = "application/pdf";
        string fileName = $"relatorio-consolidado-diario-{requisicao.Data:yyyy-MM-dd}.pdf";

        return File(resposta.RelatorioPdf, contentType, fileName);

    }
}
