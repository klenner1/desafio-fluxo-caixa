using Aplicacao.Geradores;
using Aplicacao.Repositorios;
using Infraestrutura.PDFs;
using Infraestrutura.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura;

public static class Setup
{
    public static IServiceCollection AddInfraestrutura(this IServiceCollection provider)
    {
        provider.AddTransient<ISaldoDiarioConsolidadoGeradorPdf, SaldoDiarioConsolidadoGeradorPdf>();
        provider.AddTransient<ILancamentoRepositorio, LancamentoRepositorio>();

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        return provider;
    }
}
