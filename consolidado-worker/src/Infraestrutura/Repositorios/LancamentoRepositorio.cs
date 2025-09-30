using Aplicacao.Repositorios;
using Dominio.Entidades;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class LancamentoRepositorio(FluxoCaixaDbContext context) : ILancamentoRepositorio
{
    private readonly DbSet<Lancamento> _set = context.Set<Lancamento>();

    public async Task Inserir(Lancamento aggregate, CancellationToken cancellationToken)
        => await _set.AddAsync(aggregate, cancellationToken);

    public async Task<bool> Existe(Guid id, CancellationToken cancellationToken)
        => await _set.AnyAsync(x => x.Id == id, cancellationToken);


}
