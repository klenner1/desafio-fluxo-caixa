using Aplicacao.CasoDeUsos.Lancamentos.Listar;
using Aplicacao.Repositorios;
using Dominio.Entidades;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

public class LancamentoRepositorio(FluxoCaixaDbContext context) : ILancamentoRepositorio
{
    private readonly DbSet<Lancamento> _set = context.Set<Lancamento>();

    public async Task Inserir(Lancamento aggregate, CancellationToken cancellationToken) => await _set.AddAsync(aggregate, cancellationToken);

    public async Task<IEnumerable<Lancamento>> Listar(FiltroLancamento filtro, CancellationToken cancellationToken)
    {
        IQueryable<Lancamento> query = _set;
        if (filtro.Tipos is not null)
            query = query.Where(x => filtro.Tipos.Contains(x.Tipo));

        if (filtro.DataCriacaoDe is not null)
            query = query.Where(x => x.DataCriacao >= filtro.DataCriacaoDe);

        if (filtro.DataCriacaoAte is not null)
            query = query.Where(x => x.DataCriacao <= filtro.DataCriacaoAte);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}
