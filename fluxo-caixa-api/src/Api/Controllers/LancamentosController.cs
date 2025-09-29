using Aplicacao.CasoDeUsos.Lancamentos.Comum;
using Aplicacao.CasoDeUsos.Lancamentos.Criar;
using Aplicacao.CasoDeUsos.Lancamentos.Listar;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LancamentosController(ILogger<LancamentosController> logger) : ControllerBase
{

    [HttpPost()]
    public async Task<ActionResult<IEnumerable<LancamentoModeloResposta>>> Get(
        [FromServices] ICriarLancamento listar,
        [FromBody] CriarLancamentoRequisicao listarLancamento,
        CancellationToken cancellationToken)
    {
        var lista = await listar.Executar(listarLancamento, cancellationToken);
        return Ok(lista);
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<IEnumerable<LancamentoModeloResposta>>> Get(
        [FromServices] IListarLancamento listar,
        [FromQuery] ListarLancamentoRequisicao listarLancamento,
        CancellationToken cancellationToken)
    {
        var lista = await listar.Executar(listarLancamento, cancellationToken);
        return Ok(lista);
    }
}
