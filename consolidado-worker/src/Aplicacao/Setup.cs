using Aplicacao.CasoDeUsos.Lancamentos.Processar;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacao;

public static class Setup
{
    public static IServiceCollection AddAplicacao(this IServiceCollection provider)
    {
        provider.AddTransient<IProcessarLancamento, ProcessarLancamento>();

        return provider;
    }
}
