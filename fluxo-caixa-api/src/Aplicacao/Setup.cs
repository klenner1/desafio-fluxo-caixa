using Aplicacao.CasoDeUsos.Lancamentos.Criar;
using Aplicacao.CasoDeUsos.Lancamentos.Listar;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacao;

public static class Setup
{
    public static IServiceCollection AddAplicacao(this IServiceCollection provider)
    {
        provider.AddTransient<ICriarLancamento, CriarLancamento>();
        provider.AddTransient<IListarLancamento, ListarLancamento>();

        return provider;
    }
}
