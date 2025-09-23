namespace Perfumes.WebAPI.Endpoints
{
    /// <summary>
    /// Classe onde se localizam os Endpoints da tabela de perfumes da API
    /// </summary>
    public static class EndpointsBuilderExtensionsPerfumes
    {
        /// <summary>
        /// Endpoints da tabela de perfumes
        /// </summary>
        /// <param name="app">Recebe como parâmetro a estrutura central da aplicação Web</param>
        public static void MapPerfumesEndpoints(this IEndpointRouteBuilder app)
        {
            #region Consultas
            app.MapGet("/perfumes", PerfumesHandlers.ObterTodos).WithOpenApi();

            app.MapGet("/perfumes/porId", PerfumesHandlers.BuscaPorId).WithOpenApi();

            app.MapGet("/perfumesLinQ/porNome/{nome}", PerfumesHandlers.BuscaPorNome).WithOpenApi();
            #endregion

            #region Inserções, modificações e deleções
            app.MapPost("/perfumes", PerfumesHandlers.InserePerfume).WithOpenApi();

            app.MapPut("/perfumes", PerfumesHandlers.ModificaPerfume).WithOpenApi();

            app.MapDelete("/perfumes/{perfumeId}", PerfumesHandlers.RemovePerfume).WithOpenApi();
            #endregion
        }
    }
}
