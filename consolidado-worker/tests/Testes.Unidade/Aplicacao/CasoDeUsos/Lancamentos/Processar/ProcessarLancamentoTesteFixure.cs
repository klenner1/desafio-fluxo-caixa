using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Moq;
using Testes.Unidade.Comum;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Processar;

public class ProcessarLancamentoTesteFixure : LancamentoTesteFixure
{
    public static Mock<ILancamentoRepositorio> MockRepositorio => new();
    public static Mock<IUnidadeDeTrabalho> MockUnidadeDeTrabalho => new();
}

[CollectionDefinition(nameof(ProcessarLancamentoTesteFixure))]
public class CriarLancamentoTesteFixureCollection
    : ICollectionFixture<ProcessarLancamentoTesteFixure>
{ }