using Aplicacao.CasoDeUsos.Lancamentos.Processar;
using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Dominio.Entidades;

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
            lancamentoFixure.IdValido,
            lancamentoFixure.DescricaoValida,
            lancamentoFixure.ValorValido,
            lancamentoFixure.TipoValido,
            lancamentoFixure.DataCriacaoValida);

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
}
