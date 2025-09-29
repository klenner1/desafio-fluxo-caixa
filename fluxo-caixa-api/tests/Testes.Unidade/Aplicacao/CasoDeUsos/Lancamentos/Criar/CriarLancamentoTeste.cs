using Aplicacao.CasoDeUsos.Lancamentos.Criar;
using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Entidades;
using Dominio.Exceptions;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Criar;

[Collection(nameof(CriarLancamentoTesteFixure))]
[Trait("Aplicacao", "CriarLancamento - Caso de uso")]
public class CriarLancamentoTeste(CriarLancamentoTesteFixure lancamentoFixure)
{

    private readonly CriarLancamentoTesteFixure lancamentoFixure = lancamentoFixure;

    [Fact(DisplayName = nameof(CriarLancamento))]
    public async Task CriarLancamento()
    {
        var mockRepositorio = CriarLancamentoTesteFixure.MockRepositorio;
        var mockUnidadeDetrabalho = CriarLancamentoTesteFixure.MockUnidadeDeTrabalho;
        var casoDeUso = new CriarLancamento(
            mockRepositorio.Object,
            mockUnidadeDetrabalho.Object);

        var requisicao = new CriarLancamentoRequisicao(
            lancamentoFixure.DescricaoValida,
            lancamentoFixure.ValorValido,
            lancamentoFixure.TipoValido);

        var resposta = await casoDeUso.Executar(requisicao, CancellationToken.None);

        resposta.Should().NotBeNull();
        resposta.Id.Should().NotBeEmpty();
        resposta.Descricao.Should().Be(requisicao.Descricao);
        resposta.Tipo.Should().Be(requisicao.Tipo);
        resposta.DataCriacao.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));

        mockRepositorio.Verify(
            r => r.Inserir(It.IsAny<Lancamento>(), It.IsAny<CancellationToken>()),
            Times.Once,
            $"{nameof(ILancamentoRepositorio)}.{nameof(ILancamentoRepositorio.Inserir)} deve ser chamado");

        mockUnidadeDetrabalho.Verify(
            r => r.Commit(It.IsAny<CancellationToken>()),
            Times.Once,
            $"{nameof(IUnidadeDeTrabalho)}.{nameof(IUnidadeDeTrabalho.Commit)} deve ser chamado");
    }


    [Theory(DisplayName = nameof(DeveLancarExecaoQuandoEntidadeInvalida))]
    [MemberData(nameof(GetInvalidInputs))]
    public async Task DeveLancarExecaoQuandoEntidadeInvalida(
        CriarLancamentoRequisicao requisicao,
        string mensagemDeErro)
    {
        var mockRepositorio = CriarLancamentoTesteFixure.MockRepositorio;
        var mockUnidadeDetrabalho = CriarLancamentoTesteFixure.MockUnidadeDeTrabalho;
        var casoDeUso = new CriarLancamento(
            mockRepositorio.Object,
            mockUnidadeDetrabalho.Object);

        var act = async () => await casoDeUso.Executar(requisicao, CancellationToken.None);

        await act.Should().ThrowAsync<ValidacaoEntidadeException>()
            .WithMessage(mensagemDeErro);

        mockRepositorio.Verify(
            r => r.Inserir(It.IsAny<Lancamento>(), It.IsAny<CancellationToken>()),
            Times.Never,
            $"{nameof(ILancamentoRepositorio)}.{nameof(ILancamentoRepositorio.Inserir)} não deve ser chamado");

        mockUnidadeDetrabalho.Verify(
            r => r.Commit(It.IsAny<CancellationToken>()),
            Times.Never,
            $"{nameof(IUnidadeDeTrabalho)}.{nameof(IUnidadeDeTrabalho.Commit)} não deve ser chamado");
    }

    public static TheoryData<CriarLancamentoRequisicao, string> GetInvalidInputs()
    {
        var fixure = new CriarLancamentoTesteFixure();
        return new()
        {
            {
                new CriarLancamentoRequisicao(null!,fixure.ValorValido,fixure.TipoValido ),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ser nulo"
            },
            {
                new CriarLancamentoRequisicao(fixure.DescricaoInvalidaTamanhoMaiorQue255,fixure.ValorValido,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho maior que 255 caracteres"
            },
            {
                new CriarLancamentoRequisicao(fixure.DescricaoInvalidaTamanhoMenorQue3,fixure.ValorValido,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho menor que 3 caracteres"
            },
            {
                new CriarLancamentoRequisicao(fixure.DescricaoValida,fixure.ValorInvalidoMenorQue0,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Valor)}: Deve ser maior ou igual a 0"
            },
            {
                new CriarLancamentoRequisicao(fixure.DescricaoValida,fixure.ValorValido,fixure.TipoInvalido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Tipo)}: Possui um intervalo de valores que não inclui o valor informado"
            }
        };
    }

}
