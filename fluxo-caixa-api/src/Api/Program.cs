using Api.Extensions;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;
using Aplicacao;
using Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAplicacao();
builder.Services.AddInfraestrutura();

var connectionString = builder.Configuration.GetValue<string>("pgsqlConnectionString");

builder.Services.AddDbContext<FluxoCaixaDbContext>(opt =>
    opt.UseNpgsql(connectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(op=>op.SwaggerEndpoint("/openapi/v1.json","Fluxo Caixa"));
    app.AplicarMigracao();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
