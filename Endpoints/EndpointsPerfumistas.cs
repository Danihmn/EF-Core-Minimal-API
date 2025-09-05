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
                // Retorna todos os perfumistas, ordenados pelos seus nomes
                return context.Perfumistas
                .Include(perfumista => perfumista.Perfumes)
                .OrderBy(perfumista => perfumista.Nome)
                .ToList();
            })
            .WithOpenApi();

            // Acessa apenas o primeiro perfumista da tabela
            app.MapGet("/perfumistas/primeiroPerfumista", (Context context) =>
            {
                return context.Perfumistas
                .Include(perfume => perfume.Perfumes)
                .FirstOrDefault();
            })
            .WithOpenApi();

            // Acessa apenas o ultimo perfumista da tabela
            app.MapGet("/perfumistas/ultimoPerfumista", (Context context) =>
            {
                return context.Perfumistas
                .Include(perfume => perfume.Perfumes)
                .OrderBy(perfumista => perfumista.Nome)
                .LastOrDefault();

                // Formas de acessar o último da tabela:
                // OrderBy().lastOrDefault()
                // OrderByDescending().FirstOrDefault()
            })
            .WithOpenApi();

            // Acessa o perfumista pelo seu Id
            app.MapGet("/perfumistas/{id}", (Context context, int id) =>
            {
                return context.Perfumistas
                .Where(diretor => diretor.Id == id)
                .Include(perfumista => perfumista.Perfumes)
                .ToList();
            })
            .WithOpenApi();

            // Acessa apenas o nome do perfumista pelo seu Id
            app.MapGet("/perfumistas/nomePerfumista{id}", (Context context, int id) =>
            {
                return context.Perfumistas
                .Where(diretor => diretor.Id == id)
                .Include(perfumista => perfumista.Perfumes)
                .Select(perfumista => perfumista.Nome)
                .ToList();
            })
            .WithOpenApi();

            // Acessa o perfumista pelo seu nome com EF Core Funcions
            app.MapGet("/perfumistasEFFunctions/porNome/{nome}", (Context context, string nome) =>
            {
                // Utiliza função Like do EF para localizar os dados, sem a necessidade das strings terem que ser idênticas
                // Nos parâmetros da função Like, os % dizem que deve ser localizado tudo aquilo que vem antes e depois do que foi digitado
                return context.Perfumistas.Where(
                    perfumista => EF.Functions.Like(perfumista.Nome, $"%{nome}%")).Include(perfume => perfume.Perfumes).ToList();
            })
            .WithOpenApi();

            // Acessa o perfumista pelo seu nome com LinQ
            app.MapGet("/perfumistasLinQ/porNome/{nome}", (Context context, string nome) =>
            {
                // Utiliza LinQ para localizar o perfumista com base no nome escrito
                return context.Perfumistas.Where(perfumista => perfumista.Nome.Contains(nome)).Include(perfumes => perfumes.Perfumes).ToList();
            })
            .WithOpenApi();

            // Acessa apenas o primeiro perfumista da lista, através de seu nome
            app.MapGet("/perfumistas/retornaDefault/{nome}", (Context context, string nome) =>
            {
                // Caso o perfumista não for encontrado na consulta, será retornado um perfume Default, que é instanciado
                return context.Perfumistas
                .Include(perfumes => perfumes.Perfumes)
                .FirstOrDefault(perfumista => perfumista.Nome.Contains(nome)) ??
                new Perfumista
                {
                    Id = 404,
                    Nome = "Giorgio Armani"
                };
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
