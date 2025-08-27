using Perfumes.WebAPI.Contexto;
using Perfumes.WebAPI.Entidades;

namespace Perfumes.WebAPI.Endpoints
{
    /// <summary>
    /// Classe onde se localizam os Endpoints da tabela de perfumes da API
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
    }
}
