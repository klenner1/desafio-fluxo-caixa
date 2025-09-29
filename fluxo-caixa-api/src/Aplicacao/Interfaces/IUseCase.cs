namespace Aplicacao.Interfaces;

public interface IUseCase<TRequisicao, TResopsta>
    where TRequisicao : IRequisicao<TResopsta>
{
    Task<TResopsta> Executar(TRequisicao requisicao, CancellationToken cancellationToken);
}

public interface IUseCase<in TRequisicao>
    where TRequisicao : IRequisicao
{
    Task Executar(TRequisicao requisicao);
}