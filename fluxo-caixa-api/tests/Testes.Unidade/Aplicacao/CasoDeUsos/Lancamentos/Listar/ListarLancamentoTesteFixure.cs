using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Moq;
using Testes.Unidade.Comum;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Listar;

public class ListarLancamentoTesteFixure : LancamentoTesteFixure
{
    public static Mock<ILancamentoRepositorio> MockRepositorio => new();
}

[CollectionDefinition(nameof(ListarLancamentoTesteFixure))]
public class CriarLancamentoTesteFixureCollection
    : ICollectionFixture<ListarLancamentoTesteFixure>
{ }