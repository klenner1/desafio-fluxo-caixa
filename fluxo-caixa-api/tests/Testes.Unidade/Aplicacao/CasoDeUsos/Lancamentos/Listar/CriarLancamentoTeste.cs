using Aplicacao.CasoDeUsos.Lancamentos.Listar;
using Aplicacao.Repositorios;
using Dominio.Entidades;
using Dominio.Enum;

namespace Testes.Unidade.Aplicacao.CasoDeUsos.Lancamentos.Listar;

[Collection(nameof(ListarLancamentoTesteFixure))]
[Trait("Aplicacao", "ListarLancamentos - Caso de uso")]
public class CriarLancamentoTeste(ListarLancamentoTesteFixure lancamentoFixure)
{

    private readonly ListarLancamentoTesteFixure _lancamentoFixure = lancamentoFixure;

    [Fact(DisplayName = nameof(CriarLancamento))]
    public async Task CriarLancamento()
    {

        var mockRepositorio = ListarLancamentoTesteFixure.MockRepositorio;
        var retornoRepositorio = new List<Lancamento> {
            _lancamentoFixure.LancamentoValido,
            _lancamentoFixure.LancamentoValido,
            _lancamentoFixure.LancamentoValido
        };
        mockRepositorio
            .Setup(x => x.Listar(It.IsAny<FiltroLancamento>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(retornoRepositorio);

        var casoDeUso = new ListarLancamento(mockRepositorio.Object);

        var requisicao = new ListarLancamentoRequisicao(
            _lancamentoFixure.Faker.Random.EnumValues<ETipoLancamento>(),
            _lancamentoFixure.Faker.Date.RecentOffset(),
            DateTimeOffset.UtcNow);

        var resposta = await casoDeUso.Executar(requisicao, CancellationToken.None);

        resposta.Should().NotBeNull();
        resposta.Should().HaveSameCount(retornoRepositorio);
        resposta.Should().AllSatisfy(x =>
        {
            x.Id.Should().NotBeEmpty();
            var lancamentoRepo = retornoRepositorio.Find(y => y.Id == x.Id);
            lancamentoRepo.Should().NotBeNull();

            x.Descricao.Should().Be(lancamentoRepo.Descricao);
            x.Tipo.Should().Be(lancamentoRepo.Tipo);
            x.DataCriacao.Should().Be(lancamentoRepo.DataCriacao);
        });

        mockRepositorio.Verify(
            r => r.Listar(It.IsAny<FiltroLancamento>(), It.IsAny<CancellationToken>()),
            Times.Once,
            $"{nameof(ILancamentoRepositorio)}.{nameof(ILancamentoRepositorio.Listar)} deve ser chamado");
    }


}
