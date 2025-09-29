using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Moq;
using Testes.Unidade.Comum;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Criar;

public class CriarLancamentoTesteFixure : LancamentoTesteFixure
{
    public static Mock<ILancamentoRepositorio> MockRepositorio => new();
    public static Mock<IUnidadeDeTrabalho> MockUnidadeDeTrabalho => new();
}

[CollectionDefinition(nameof(CriarLancamentoTesteFixure))]
public class CriarLancamentoTesteFixureCollection
    : ICollectionFixture<CriarLancamentoTesteFixure>
{ }