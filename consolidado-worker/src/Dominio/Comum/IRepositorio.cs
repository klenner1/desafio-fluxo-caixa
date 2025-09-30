namespace Dominio.Comum;

public interface IRepositorio<in TRaizDeAgregacao>
    where TRaizDeAgregacao : RaizDeAgregacao
{
    Task Inserir(TRaizDeAgregacao aggregate, CancellationToken cancellationToken);
}
