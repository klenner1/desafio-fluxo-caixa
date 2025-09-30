using Dominio.Entidades;
using Dominio.Exceptions;

namespace Testes.Unidade.Dominio.Entidades.Lancamentos;

[Collection(nameof(LancamentoEntidadeTesteFixure))]
[Trait("Dominio", "Lancamento - Agregador")]
public class LancamentoTest(LancamentoEntidadeTesteFixure lancamentoTesteFixure)
{

    private readonly LancamentoEntidadeTesteFixure _lancamentoTesteFixure = lancamentoTesteFixure;

    [Fact(DisplayName = nameof(CriarInstancia))]
    public void CriarInstancia()
    {
        //Arrange
        var id = _lancamentoTesteFixure.IdValido;
        var descricao = _lancamentoTesteFixure.DescricaoValida;
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoValido;
        var dataCriacao = _lancamentoTesteFixure.DataCriacaoValida;
        //Act
        var lancamento = new Lancamento()
        {
            Id = id,
            Descricao = descricao,
            Valor = valor,
            Tipo = tipo,
            DataCriacao = dataCriacao,
        };

        //Assert
        lancamento.Should().NotBeNull();
        lancamento.Id.Should().Be(id);
        lancamento.Descricao.Should().Be(descricao);
        lancamento.Valor.Should().Be(valor);
        lancamento.Tipo.Should().Be(tipo);
        lancamento.DataCriacao.Should().Be(dataCriacao);
    }
}
