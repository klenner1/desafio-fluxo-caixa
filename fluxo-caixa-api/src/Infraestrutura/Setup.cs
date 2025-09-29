using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Infraestrutura.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura;

public static class Setup
{
    public static IServiceCollection AddInfraestrutura(this IServiceCollection provider)
    {
        provider.AddTransient<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        provider.AddTransient<ILancamentoRepositorio, LancamentoRepositorio>();

        return provider;
    }
}
