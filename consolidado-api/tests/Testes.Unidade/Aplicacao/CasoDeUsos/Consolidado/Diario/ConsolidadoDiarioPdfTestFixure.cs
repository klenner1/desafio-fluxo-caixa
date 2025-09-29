using Aplicacao.Geradores;
using Aplicacao.Repositorios;
using Moq;
using Testes.Unidade.Comum;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Consolidado.Diario;

public class ConsolidadoDiarioPdfTestFixure : LancamentoTesteFixure
{
    public static Mock<ILancamentoRepositorio> MockRepositorio => new();
    public static Mock<ISaldoDiarioConsolidadoGeradorPdf> MockGeradorPdf => new();
}

[CollectionDefinition(nameof(ConsolidadoDiarioPdfTestFixure))]
public class CriarLancamentoTesteFixureCollection
    : ICollectionFixture<ConsolidadoDiarioPdfTestFixure>
{ }