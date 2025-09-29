using Dominio.Comum;
using Dominio.Entidades.Validadores;
using Dominio.Enum;
using Dominio.Exceptions;

namespace Dominio.Entidades;

public class Lancamento : RaizDeAgregacao
{
    private Lancamento()
    {
    }

    private Lancamento(string descricao, decimal valor, ETipoLancamento tipo)
    {
        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
    }

    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public ETipoLancamento Tipo { get; set; }



    public static Lancamento Criar(string descricao, decimal valor, ETipoLancamento tipo)
    {
        var lancamento = new Lancamento(descricao, valor, tipo);
        lancamento.Validate();
        return lancamento;
    }

    private void Validate()
    {
        var validador = new LancamentoValidador();
        var resultado = validador.Validate(this);
        if (!resultado.IsValid)
            throw new ValidacaoEntidadeException(resultado.Errors[0].ErrorMessage);
    }
}
