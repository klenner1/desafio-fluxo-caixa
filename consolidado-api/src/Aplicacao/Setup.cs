using Aplicacao.CasoDeUsos.Consolidado.Diario;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacao;

public static class Setup
{
    public static IServiceCollection AddAplicacao(this IServiceCollection provider)
    {
        provider.AddTransient<IConsolidadoDiarioPdf, ConsolidadoDiarioPdf>();

        return provider;
    }
}
