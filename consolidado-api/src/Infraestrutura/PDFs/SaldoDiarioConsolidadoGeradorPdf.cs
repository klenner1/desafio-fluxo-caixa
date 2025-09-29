using Aplicacao.Geradores;
using Dominio.Entidades;
using Dominio.Enum;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infraestrutura.PDFs;

public class SaldoDiarioConsolidadoGeradorPdf : ISaldoDiarioConsolidadoGeradorPdf
{
    public byte[] Gerar(IEnumerable<Lancamento> lancamentos)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header()
                        .Text("Relatório de saldo diário consolidado")
                        .SemiBold()
                        .FontSize(16)
                        .AlignCenter();

                page.Content().Element(x => ComporConteudo(x, lancamentos));

                page.Footer()
                    .Text($"{DateTimeOffset.Now:F}")
                    .AlignEnd();
            });
        });

        return document.GeneratePdf();
    }

    private static void ComporConteudo(IContainer container, IEnumerable<Lancamento> lancamentos)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(x => ComporTabela(x, lancamentos));

            column.Item().Row(x => ComporTotalizador(x, lancamentos));

        });
    }

    public static void ComporTotalizador(RowDescriptor row, IEnumerable<Lancamento> lancamentos)
    {
        var valorTotal = lancamentos.Sum(x => x.ValorCompensado);
        row.RelativeItem();
        row.AutoItem()
            .Text($"Saldo do dia:")
            .FontSize(14);
        row.ConstantItem(5);
        var valorItem = row.AutoItem()
            .Text($"{valorTotal:c}")
            .FontSize(14);
        if (valorTotal < 0)
            valorItem.FontColor(Colors.Red.Medium);
    }

    private static void ComporTabela(IContainer container, IEnumerable<Lancamento> lancamentos)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(45);
                columns.ConstantColumn(60);
                columns.RelativeColumn();
                columns.ConstantColumn(130);
            });

            table.Header(header =>
            {
                header.CelulaCabecalho("#");
                header.CelulaCabecalho("Hora");
                header.CelulaCabecalho("Descrição");
                header.CelulaCabecalho("Valor");
            });

            foreach (var (lancamento, i) in lancamentos.Select((l, i) => (l, i)))
            {
                table.Celula($"{i + 1}");
                table.Celula($"{lancamento.DataCriacao:HH:mm}");
                table.Celula($"{lancamento.Descricao}");
                var celularValor = table.Celula($"{lancamento.ValorCompensado:C}")
                    .AlignEnd();
                if (lancamento.Tipo == ETipoLancamento.Saida)
                    celularValor.FontColor(Colors.Red.Medium);
            }

        });
    }
}
public static class SaldoDiarioConsolidadoGeradorPdf2
{
    public static TextBlockDescriptor Celula(this TableDescriptor table, string valor)
        => table.Cell()
        .Element(EstiloCelulaLinhaTabela)
        .Text(valor);

    static IContainer EstiloCelulaLinhaTabela(IContainer container)
    {
        return container
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .PaddingVertical(5)
            .PaddingHorizontal(10);
    }

    public static TextBlockDescriptor CelulaCabecalho(this TableCellDescriptor table, string valor)
       => table.Cell()
       .Element(EstiloCelulaCabecalhoTabela)
       .Text(valor);

    static IContainer EstiloCelulaCabecalhoTabela(IContainer container)
    {
        return container
                       .DefaultTextStyle(x => x.SemiBold())
                       .PaddingVertical(5)
                       .BorderBottom(1)
                       .BorderColor(Colors.Black);
    }

}