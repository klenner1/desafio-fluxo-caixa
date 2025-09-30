using Aplicacao;
using Infraestrutura;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddAplicacao();
        services.AddInfraestrutura(context.Configuration);
    })
    .Build();

await host.RunAsync();


