using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Contextos;

public class FluxoCaixaDbContext(DbContextOptions<FluxoCaixaDbContext> dbContextOptions)
    : DbContext(dbContextOptions)
{
    public DbSet<Lancamento> Lancamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lancamento>()
            .HasKey(f => f.Id);
    }
}
