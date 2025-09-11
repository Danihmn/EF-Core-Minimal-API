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
                // Retorna todos os perfumes, ordenados pelos seus nomes, e após, pelos seus valores
                return context.Perfumes
                .Include(p => p.Perfumista)
                .OrderBy(perfume => perfume.Nome)
                .ThenBy(perfume => perfume.Valor)
                .ToList();
            })
            .WithOpenApi();

            // Acessa apenas o primeiro perfume da tabela
            app.MapGet("/perfumes/primeiroPerfume", (Context context) =>
            {
                return context.Perfumes
                .Include(p => p.Perfumista)
                .FirstOrDefault();
            })
            .WithOpenApi();

            // Acessa apenas o ultimo perfume da tabela
            app.MapGet("/perfumes/ultimoPerfume", (Context context) =>
            {
                return context.Perfumes
                .Include(p => p.Perfumista)
                .OrderByDescending(perfume => perfume.Nome)
                .FirstOrDefault();

                // Formas de acessar o último da tabela:
                // OrderBy().lastOrDefault()
                // OrderByDescending().FirstOrDefault()
            })
            .WithOpenApi();

            // Acessa o perfume através do seu Id
            app.MapGet("/perfumes/{perfumeId}", (Context context, int id) =>
            {
                return context.Perfumes
                .Where(perfume => perfume.Id == id)
                .Include(perfumista => perfumista.Perfumista)
                .ToList();
            })
            .WithOpenApi();

            // Acessa apenas o nome do perfume através do seu Id
            app.MapGet("/perfumes/apenasNome{perfumeId}", (Context context, int id) =>
            {
                return context.Perfumes
                .Where(perfume => perfume.Id == id)
                .Include(perfumista => perfumista.Perfumista)
                .Select(perfume => perfume.Nome)
                .ToList();
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

            // Acessa apenas o primeiro perfume da lista, através de seu nome
            app.MapGet("/perfumes/retornaDefault/{nome}", (Context context, string nome) =>
            {
                // Caso o perfume não for encontrado na consulta, será retornado um perfume Default, que é instanciado
                return context.Perfumes
                .Include(perfumista => perfumista.Perfumista)
                .FirstOrDefault(perfume => perfume.Nome.Contains(nome)) ??
                new Perfume
                {
                    Id = 404,
                    Marca = "Emporio Armani",
                    Nome = "Stronger With You",
                    Tipo = "Eau de Parfum",
                    Valor = 800,
                    PerfumistaId = 404,
                    Perfumista = new Perfumista { Id = 404, Nome = "Giorgio Armani" }
                };
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

            // Modifica apenas uma parte, com base em seu Id, com Update();
            app.MapPatch("/perfumes/update", (Context context, PerfumeUpdate perfumeUpdate) =>
            {
                var perfume = context.Perfumes.Find(perfumeUpdate.Id);

                if (perfume == null)
                    return Results.NotFound("Perfume não encontrado");

                perfume.Marca = perfumeUpdate.Marca;
                perfume.Nome = perfumeUpdate.Nome;
                perfume.Tipo = perfumeUpdate.Tipo;

                context.Perfumes.Update(perfume);
                context.SaveChanges();

                return Results.Ok();
            })
            .WithOpenApi();

            // Modifica apenas uma parte, com base em seu Id, com ExecuteUpdate();
            app.MapPatch("/perfumes/executeUpdate", (Context context, PerfumeUpdate perfumeUpdate) =>
            {
                var linhasAfetadas = context.Perfumes
                .Where(perfume => perfume.Id == perfumeUpdate.Id)
                .ExecuteUpdate(setter => setter
                    .SetProperty(perfume => perfume.Marca, perfumeUpdate.Marca)
                    .SetProperty(perfume => perfume.Nome, perfumeUpdate.Nome)
                );

                if (linhasAfetadas > 0)
                    return Results.Ok($"Você teve um total de {linhasAfetadas} linha(s) afetada(s)");
                else
                    return Results.NoContent();
            })
            .WithOpenApi();

            // Deleta perfumes com base em seus Ids
            app.MapDelete("/perfumes/{perfumeId}", (int perfumeId, Context context) =>
            {
                context.Perfumes
                .Where(perfume => perfume.Id == perfumeId)
                .ExecuteDelete<Perfume>();
            })
            .WithOpenApi();
            #endregion
        }
    }
}
