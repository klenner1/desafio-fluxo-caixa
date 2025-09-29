using Dominio.Entidades;

namespace Aplicacao.Geradores;

public interface ISaldoDiarioConsolidadoGeradorPdf
{
    byte[] Gerar(IEnumerable<Lancamento> lancamentos);
}
