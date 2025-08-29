using Microsoft.EntityFrameworkCore;
using Perfumes.WebAPI.Entidades;

namespace Perfumes.WebAPI.Contexto
{
    /// <summary>
    /// Sessão onde o EF Core trabalha.
    /// Responsável por inserir no Context o registro de DI e a declaração da tabela de perfumes
    /// </summary>
    public class Context : DbContext
    {
        // Sempre que for chamado, será o que foi registrado para usar injeção de dependência
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Perfume>? Perfumes { get; set; }
        public DbSet<Perfumista>? Perfumistas { get; set; }

        // Reforça o mapeamento do banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfume>()
                .HasOne(p => p.Perfumista)
                .WithMany(pf => pf.Perfumes)
                .HasForeignKey(p => p.PerfumistaId).IsRequired();
        }
    }
}
