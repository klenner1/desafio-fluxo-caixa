using Aplicacao.Repositorios;
using Dominio.Entidades;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Infraestrutura.Repositorios;

public class LancamentoRepositorio(FluxoCaixaDbContext context) : ILancamentoRepositorio
{
    private readonly DbSet<Lancamento> _set = context.Set<Lancamento>();

    public async Task Inserir(Lancamento aggregate, CancellationToken cancellationToken)
        => await _set.AddAsync(aggregate, cancellationToken);

    public async Task<IEnumerable<Lancamento>> ListarDiario(DateOnly data, CancellationToken cancellationToken)
        => await _set
            .Where(x =>
                x.DataCriacao.Year == data.Year
                && x.DataCriacao.Month == data.Month
                && x.DataCriacao.Day == data.Day
                )
            .ToListAsync(cancellationToken);
}
