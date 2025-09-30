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
        var descricao = _lancamentoTesteFixure.DescricaoValida;
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoValido;
        //Act
        var lancamento = Lancamento.Criar(descricao, valor, tipo);

        //Assert
        lancamento.Should().NotBeNull();
        lancamento.Id.Should().NotBeEmpty();
        lancamento.Descricao.Should().Be(descricao);
        lancamento.Valor.Should().Be(valor);
        lancamento.Tipo.Should().Be(tipo);
        lancamento.DataCriacao.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = nameof(CriarInstanciaErroQuandoDescricaoNull))]
    public void CriarInstanciaErroQuandoDescricaoNull()
    {
        //Arrange
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoValido;
        //Act
        var act = () => Lancamento.Criar(null!, valor, tipo);

        //Assert
        act.Should().Throw<ValidacaoEntidadeException>()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ser nulo");
    }

    [Fact(DisplayName = nameof(CriarInstanciaErroQuandoDescricaoMenorQue3Caracteres))]
    public void CriarInstanciaErroQuandoDescricaoMenorQue3Caracteres()
    {
        //Arrange
        var descricao = _lancamentoTesteFixure.DescricaoInvalidaTamanhoMenorQue3;
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoValido;
        //Act
        var act = () => Lancamento.Criar(descricao, valor, tipo);

        //Assert
        act.Should().Throw<ValidacaoEntidadeException>()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho menor que 3 caracteres");
    }


    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        //Arrange
        var descricao = _lancamentoTesteFixure.DescricaoInvalidaTamanhoMaiorQue255;
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoValido;
        //Act
        var act = () => Lancamento.Criar(descricao, valor, tipo);

        //Assert
        act.Should().Throw<ValidacaoEntidadeException>()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho maior que 255 caracteres");
    }

    [Fact(DisplayName = nameof(CriarInstanciaErroQuandoValorENegativo))]
    public void CriarInstanciaErroQuandoValorENegativo()
    {
        //Arrange
        var descricao = _lancamentoTesteFixure.DescricaoValida;
        var valor = _lancamentoTesteFixure.ValorInvalidoMenorQue0;
        var tipo = _lancamentoTesteFixure.TipoValido;
        //Act
        var act = () => Lancamento.Criar(descricao, valor, tipo);

        //Assert
        act.Should().Throw<ValidacaoEntidadeException>()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Valor)}: Deve ser maior ou igual a 0");
    }


    [Fact(DisplayName = nameof(CriarInstanciaErroQuandoTipoForaRangeDeETipoLancamento))]
    public void CriarInstanciaErroQuandoTipoForaRangeDeETipoLancamento()
    {
        //Arrange
        var descricao = _lancamentoTesteFixure.DescricaoValida;
        var valor = _lancamentoTesteFixure.ValorValido;
        var tipo = _lancamentoTesteFixure.TipoInvalido;
        //Act
        var act = () => Lancamento.Criar(descricao, valor, tipo);

        //Assert
        act.Should().Throw<ValidacaoEntidadeException>()
            .WithMessage($"{nameof(Lancamento)}.{nameof(Lancamento.Tipo)}: Possui um intervalo de valores que não inclui o valor informado");
    }


}
