using Aplicacao.CasoDeUsos.Consolidado.Diario;
using Aplicacao.Geradores;
using Aplicacao.Repositorios;
using Dominio.Entidades;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Consolidado.Diario;

[Collection(nameof(ConsolidadoDiarioPdfTestFixure))]
[Trait("Aplicacao", "CriarLancamento - Caso de uso")]
public class ConsolidadoDiarioPdfTest(ConsolidadoDiarioPdfTestFixure fixure)
{

    private readonly ConsolidadoDiarioPdfTestFixure _fixure = fixure;

    [Fact(DisplayName = nameof(CrarRelatorio))]
    public async Task CrarRelatorio()
    {
        var mockRepositorio = ConsolidadoDiarioPdfTestFixure.MockRepositorio;
        mockRepositorio
            .Setup(x => x.ListarDiario(It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([
                _fixure.LancamentoValido,
                _fixure.LancamentoValido,
                _fixure.LancamentoValido
                ]);

        var mockGeradorPdf = ConsolidadoDiarioPdfTestFixure.MockGeradorPdf;
        mockGeradorPdf
           .Setup(x => x.Gerar(It.IsAny<IEnumerable<Lancamento>>()))
           .Returns(_fixure.Faker.Random.Bytes(100_000));

        var casoDeUso = new ConsolidadoDiarioPdf(
            mockRepositorio.Object,
            mockGeradorPdf.Object);

        var requisicao = new ConsolidadoDiarioPdfRequisicao(_fixure.Faker.Date.RecentDateOnly());

        var resposta = await casoDeUso.Executar(requisicao, CancellationToken.None);

        resposta.Should().NotBeNull();
        resposta.RelatorioPdf.Should().NotBeEmpty();

        mockRepositorio.Verify(
            r => r.ListarDiario(It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()),
            Times.Once,
            $"{nameof(ILancamentoRepositorio)}.{nameof(ILancamentoRepositorio.ListarDiario)} deve ser chamado");

        mockGeradorPdf.Verify(
            r => r.Gerar(It.IsAny<IEnumerable<Lancamento>>()),
            Times.Once,
            $"{nameof(ISaldoDiarioConsolidadoGeradorPdf)}.{nameof(ISaldoDiarioConsolidadoGeradorPdf.Gerar)} deve ser chamado");
    }
}
