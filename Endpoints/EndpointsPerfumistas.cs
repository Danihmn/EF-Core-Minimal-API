using Microsoft.EntityFrameworkCore;
using Perfumes.WebAPI.Contexto;
using Perfumes.WebAPI.Entidades;

namespace Perfumes.WebAPI.Endpoints
{
    /// <summary>
    /// Classe onde se localizam os Endpoints da tabela de perfumistas da API
    /// </summary>
    public static class EndpointsPerfumistas
    {
        /// <summary>
        /// Endpoints da tabela de perfumistas
        /// </summary>
        /// <param name="app">Recebe como parâmetro a estrutura central da aplicação Web</param>
        public static void MapPerfumistasEndpoints(this WebApplication app)
        {
            #region Endpoints Perfumistas
            // Acessa todos os perfumistas
            app.MapGet("/perfumistas", (Context context) =>
            {
                return context.Perfumistas.Include(perfumista => perfumista.Perfumes).ToList();
            })
            .WithOpenApi();

            // Acessa o perfumista pelo seu Id
            app.MapGet("/perfumistas/{id}", (Context context, int id) =>
            {
                return context.Perfumistas.Where(diretor => diretor.Id == id).Include(perfumista => perfumista.Perfumes).ToList();
            })
            .WithOpenApi();

            // Insere perfumistas
            app.MapPost("/perfumistas", (Perfumista perfumista, Context context) =>
            {
                context.Perfumistas.Add(perfumista);

                context.SaveChanges();
            })
            .WithOpenApi();

            // Modifica perfumistas
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

            // Deleta perfumistas
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
