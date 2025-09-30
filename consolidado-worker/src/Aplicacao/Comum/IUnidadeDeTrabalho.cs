namespace Aplicacao.Comum;

public interface IUnidadeDeTrabalho
{
    public Task Commit(CancellationToken cancellationToken);
}
