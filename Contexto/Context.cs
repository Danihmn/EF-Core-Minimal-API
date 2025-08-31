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

            #region Dados iniciais
            modelBuilder.Entity<Perfumista>().HasData(
                new Perfumista { Id = 1, Nome = "Verônica Kato" },
                new Perfumista { Id = 2, Nome = "Alberto Morillas" },
                new Perfumista { Id = 3, Nome = "Dominique Ropion" }
            );

            modelBuilder.Entity<Perfume>().HasData(
                new Perfume
                {
                    Id = 1,
                    Marca = "Natura",
                    Nome = "Essencial Elixir",
                    Tipo = "Deo Perfume",
                    Valor = 230,
                    PerfumistaId = 1
                },
                new Perfume
                {
                    Id = 2,
                    Marca = "Calvin Klein",
                    Nome = "CK One",
                    Tipo = "Eau de Toilette",
                    Valor = 199,
                    PerfumistaId = 2
                },
                new Perfume
                {
                    Id = 3,
                    Marca = "Dior",
                    Nome = "Sauvage",
                    Tipo = "Eau de Parfum",
                    Valor = 550,
                    PerfumistaId = 3
                }
            );
            #endregion
        }
    }
}
