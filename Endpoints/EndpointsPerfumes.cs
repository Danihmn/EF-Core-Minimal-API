using Perfumes.WebAPI.Contexto;
using Perfumes.WebAPI.Entidades;

namespace Perfumes.WebAPI.Endpoints
{
    /// <summary>
    /// Classe onde se localizam os Endpoints da API
    /// </summary>
    public static class EndpointsPerfumes
    {
        /// <summary>
        /// Endpoints da tabela de perfumes
        /// </summary>
        /// <param name="app">Recebe como parâmetro a estrutura central da aplicação Web</param>
        public static void MapPerfumesEndpoints(this WebApplication app)
        {
            #region Endpoints Perfumes
            app.MapGet("/perfumes", (Context context) =>
            {
                return context.Perfumes.ToList();
            })
            .WithOpenApi();

            app.MapPost("/perfumes", (Perfume perfume, Context context) =>
            {
                var perfumista = context.Perfumistas.Find(perfume.PerfumistaId);

                context.Perfumes.Add(perfume);

                context.SaveChanges();
            })
            .WithOpenApi();

            app.MapPut("/perfumes", (int id, Context context, Perfume perfumeNovo) =>
            {
                var perfume = context.Perfumes.Find(id);

                if (perfume != null)
                {
                    perfume.Marca = perfumeNovo.Marca;
                    perfume.Nome = perfumeNovo.Nome;
                    perfume.Tipo = perfumeNovo.Tipo;
                    perfume.Valor = perfumeNovo.Valor;
                }

                context.SaveChanges();
            })
            .WithOpenApi();

            app.MapDelete("/perfumes", (int id, Context context) =>
            {
                var perfume = context.Perfumes.Find(id);

                if (perfume != null)
                {
                    context.Perfumes.Remove(perfume);
                }

                context.SaveChanges();
            })
            .WithOpenApi();
            #endregion
        }

        /// <summary>
        /// Endpoints da tabela de perfumistas
        /// </summary>
        /// <param name="app">Recebe como parâmetro a estrutura central da aplicação Web</param>
        public static void MapPerfumistasEndpoints(this WebApplication app)
        {
            #region Endpoints Perfumistas
            app.MapGet("/perfumistas", (Context context) =>
            {
                return context.Perfumistas.ToList();
            })
            .WithOpenApi();

            app.MapPost("/perfumistas", (Perfumista perfumista, Context context) =>
            {
                context.Perfumistas.Add(perfumista);

                context.SaveChanges();
            })
            .WithOpenApi();

            app.MapPut("/perfumistas", (int id, Context context, Perfumista perfumistaNovo) =>
            {
                var perfumista = context.Perfumistas.Find(id);

                if (perfumista != null)
                {
                    perfumista.Nome = perfumistaNovo.Nome;
                    perfumista.Perfumes = perfumistaNovo.Perfumes;
                }

                context.SaveChanges();
            })
            .WithOpenApi();

            app.MapDelete("/perfumistas", (int id, Context context) =>
            {
                var perfumista = context.Perfumistas.Find(id);

                if (perfumista != null)
                {
                    context.Perfumistas.Remove(perfumista);
                }

                context.SaveChanges();
            })
            .WithOpenApi();
            #endregion
        }
    }
}
