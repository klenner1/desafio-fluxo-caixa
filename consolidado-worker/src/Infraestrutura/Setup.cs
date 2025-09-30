using Aplicacao.Comum;
using Aplicacao.Repositorios;
using Infraestrutura.Contextos;
using Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura;

public static class Setup
{
    public static IServiceCollection AddInfraestrutura(this IServiceCollection provider, IConfiguration configuration)
    {
        provider.AddTransient<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        provider.AddTransient<ILancamentoRepositorio, LancamentoRepositorio>();

        var connectionString = configuration.GetValue<string>("pgsqlConnectionString");

        provider.AddDbContext<FluxoCaixaDbContext>(opt =>
            opt.UseNpgsql(connectionString));

        return provider;
    }
}
