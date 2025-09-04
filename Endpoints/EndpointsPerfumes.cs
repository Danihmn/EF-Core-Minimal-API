using Microsoft.EntityFrameworkCore;
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
            // Acessa todos os perfumes
            app.MapGet("/perfumes", (Context context) =>
            {
                return context.Perfumes.Include(p => p.Perfumista).ToList();
            })
            .WithOpenApi();

            // Acessa o perfume através do seu Id
            app.MapGet("/perfumes/{id}", (Context context, int id) =>
            {
                return context.Perfumes.Where(perfume => perfume.Id == id).Include(perfumista => perfumista.Perfumista).ToList();
            })
            .WithOpenApi();

            // Acessa o perfume através do seu nome com EF Core Functions
            app.MapGet("/perfumesEFFunctions/porNome/{nome}", (Context context, string nome) =>
            {
                // Utiliza função Like do EF para localizar os dados, sem a necessidade das strings terem que ser idênticas
                // Nos parâmetros da função Like, os % dizem que deve ser localizado tudo aquilo que vem antes e depois do que foi digitado
                return context.Perfumes.Where(
                    perfume => EF.Functions.Like(perfume.Nome, $"%{nome}%")).Include(perfumista => perfumista.Perfumista).ToList();
            })
            .WithOpenApi();

            // Acessa o perfume através do seu nome com LinQ
            app.MapGet("/perfumesLinQ/porNome/{nome}", (Context context, string nome) =>
            {
                // Utiliza LinQ para localizar o perfume com base no nome escrito
                return context.Perfumes.Where(perfume => perfume.Nome.Contains(nome)).Include(perfumista => perfumista.Perfumista).ToList();
            })
            .WithOpenApi();

            // Insere perfumes
            app.MapPost("/perfumes", (Perfume perfume, Context context) =>
            {
                var perfumista = context.Perfumistas.Find(perfume.PerfumistaId);

                context.Perfumes.Add(perfume);

                context.SaveChanges();
            })
            .WithOpenApi();

            // Modifica perfumes
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

            // Deleta perfumes
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
