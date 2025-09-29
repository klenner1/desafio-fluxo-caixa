using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static  class MigrationsExtensions
{
    public static void AplicarMigracao(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<FluxoCaixaDbContext>();

        dbContext.Database.Migrate();
    }
}
