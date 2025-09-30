namespace Dominio.Comum;

public interface IEventoDespachante
{
    Task Despachar(IEvento evento);
}