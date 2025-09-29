using Dominio.Comum;
using Dominio.Enum;

namespace Dominio.Entidades;

public class Lancamento : RaizDeAgregacao
{
    private Lancamento()
    {
    }

    public Lancamento(string descricao, decimal valor, ETipoLancamento tipo)
    {
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
    }

    public string Descricao { get; protected init; }
    public decimal Valor { get; protected init; }
    public ETipoLancamento Tipo { get; protected init; }

    public decimal ValorCompensado { get => Tipo == ETipoLancamento.Entrada ? Valor : Valor * -1; }

}
