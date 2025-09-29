namespace Dominio.Comum;

public interface IRepositorio<in TRaizDeAgregacao>
    where TRaizDeAgregacao : RaizDeAgregacao
{
    Task Inserir(TRaizDeAgregacao aggregate, CancellationToken cancellationToken);
}


public interface IRepositorioLitaFiltrada<TRaizDeAgregacao,TFiltro>
    where TRaizDeAgregacao : RaizDeAgregacao
    where TFiltro :IFiltro<TRaizDeAgregacao>
{

    Task<IEnumerable<TRaizDeAgregacao>> Listar(TFiltro filtro, CancellationToken cancellationToken);
}
