using Dominio.Comum;
using Dominio.Enum;

namespace Dominio.Entidades;

public class Lancamento : RaizDeAgregacao
{
    public Lancamento()
    {
    }

    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public ETipoLancamento Tipo { get; set; }
}
