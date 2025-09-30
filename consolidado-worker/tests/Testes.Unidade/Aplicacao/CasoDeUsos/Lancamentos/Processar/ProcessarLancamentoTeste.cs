using Aplicacao.CasoDeUsos.Lancamentos.Processar;
using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Entidades;
using Dominio.Exceptions;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Processar;

[Collection(nameof(ProcessarLancamentoTesteFixure))]
[Trait("Aplicacao", "CriarLancamento - Caso de uso")]
public class ProcessarLancamentoTeste(ProcessarLancamentoTesteFixure lancamentoFixure)
{

    private readonly ProcessarLancamentoTesteFixure lancamentoFixure = lancamentoFixure;

    [Fact(DisplayName = nameof(CriarLancamento))]
    public async Task CriarLancamento()
    {
        var mockRepositorio = ProcessarLancamentoTesteFixure.MockRepositorio;
        var mockUnidadeDetrabalho = ProcessarLancamentoTesteFixure.MockUnidadeDeTrabalho;
        var casoDeUso = new ProcessarLancamento(
            mockRepositorio.Object,
            mockUnidadeDetrabalho.Object);

        var requisicao = new ProcessarLancamentoRequisicao(
            lancamentoFixure.DescricaoValida,
            lancamentoFixure.ValorValido,
            lancamentoFixure.TipoValido);

        var act = async () => await casoDeUso.Executar(requisicao, CancellationToken.None);

        await act.Should().NotThrowAsync();


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
        ProcessarLancamentoRequisicao requisicao,
        string mensagemDeErro)
    {
        var mockRepositorio = ProcessarLancamentoTesteFixure.MockRepositorio;
        var mockUnidadeDetrabalho = ProcessarLancamentoTesteFixure.MockUnidadeDeTrabalho;
        var casoDeUso = new ProcessarLancamento(
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

    public static TheoryData<ProcessarLancamentoRequisicao, string> GetInvalidInputs()
    {
        var fixure = new ProcessarLancamentoTesteFixure();
        return new()
        {
            {
                new ProcessarLancamentoRequisicao(null!,fixure.ValorValido,fixure.TipoValido ),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ser nulo"
            },
            {
                new ProcessarLancamentoRequisicao(fixure.DescricaoInvalidaTamanhoMaiorQue255,fixure.ValorValido,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho maior que 255 caracteres"
            },
            {
                new ProcessarLancamentoRequisicao(fixure.DescricaoInvalidaTamanhoMenorQue3,fixure.ValorValido,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Descricao)}: Não deve ter tamanho menor que 3 caracteres"
            },
            {
                new ProcessarLancamentoRequisicao(fixure.DescricaoValida,fixure.ValorInvalidoMenorQue0,fixure.TipoValido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Valor)}: Deve ser maior ou igual a 0"
            },
            {
                new ProcessarLancamentoRequisicao(fixure.DescricaoValida,fixure.ValorValido,fixure.TipoInvalido),
                $"{nameof(Lancamento)}.{nameof(Lancamento.Tipo)}: Possui um intervalo de valores que não inclui o valor informado"
            }
        };
    }

}
