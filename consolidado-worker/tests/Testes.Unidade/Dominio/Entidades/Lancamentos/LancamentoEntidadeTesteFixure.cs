using Testes.Unidade.Comum;

namespace Testes.Unidade.Dominio.Entidades.Lancamentos;

public class LancamentoEntidadeTesteFixure : LancamentoTesteFixure
{

}
[CollectionDefinition(nameof(LancamentoEntidadeTesteFixure))]
public class LancamentoEntidadeTesteFixureCollection
    : ICollectionFixture<LancamentoEntidadeTesteFixure>
{ }