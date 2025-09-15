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
            #region Consultas
            app.MapGet("/perfumes", (Context context) =>
            {
                return PerfumesServices.ObterTodos(context);
            })
            .WithOpenApi();

            app.MapGet("/perfumes/primeiroPerfume", (Context context) =>
            {
                return PerfumesServices.PrimeiroPerfume(context);
            })
            .WithOpenApi();

            app.MapGet("/perfumes/ultimoPerfume", (Context context) =>
            {
                return PerfumesServices.UltimoPerfume(context);
            })
            .WithOpenApi();

            app.MapGet("/perfumes/{perfumeId}", (Context context, int id) =>
            {
                return PerfumesServices.BuscaPorId(context, id);
            })
            .WithOpenApi();

            app.MapGet("/perfumes/apenasNome{perfumeId}", (Context context, int id) =>
            {
                return PerfumesServices.BuscaNomePorId(context, id);
            })
            .WithOpenApi();

            app.MapGet("/perfumesEFFunctions/porNome/{nome}", (Context context, string nome) =>
            {
                return PerfumesServices.BuscaNomeComEFFunctions(context, nome);
            })
            .WithOpenApi();

            app.MapGet("/perfumesLinQ/porNome/{nome}", (Context context, string nome) =>
            {
                return PerfumesServices.BuscaNomeComLinQ(context, nome);
            })
            .WithOpenApi();

            app.MapGet("/perfumes/retornaDefault/{nome}", (Context context, string nome) =>
            {
                return PerfumesServices.RetornaDefault(context, nome);
            })
            .WithOpenApi();
            #endregion

            #region Inserções, modificações e deleções
            app.MapPost("/perfumes", (Perfume perfume, Context context) =>
            {
                PerfumesServices.InserePerfume(context, perfume);
            })
            .WithOpenApi();

            app.MapPut("/perfumes", (int id, Context context, Perfume perfumeNovo) =>
            {
                PerfumesServices.ModificaPerfume(context, id, perfumeNovo);
            })
            .WithOpenApi();

            app.MapPatch("/perfumes/update", (Context context, PerfumeUpdate perfumeUpdate) =>
            {
                PerfumesServices.AlteraPerfumeComUpdate(context, perfumeUpdate);
            })
            .WithOpenApi();

            app.MapPatch("/perfumes/executeUpdate", (Context context, PerfumeUpdate perfumeUpdate) =>
            {
                PerfumesServices.AlteraPerfumeComExecuteUpdate(context, perfumeUpdate);
            })
            .WithOpenApi();

            app.MapDelete("/perfumes/{perfumeId}", (int perfumeId, Context context) =>
            {
                PerfumesServices.RemovePerfumePorId(context, perfumeId);
            })
            .WithOpenApi();
            #endregion
        }
    }
}
