using Perfumes.WebAPI.EndpointsHandlers;

namespace Perfumes.WebAPI.Endpoints
{
    /// <summary>
    /// Classe onde se localizam os Endpoints da tabela de perfumistas da API
    /// </summary>
    public static class EndpointsBuilderExtensionsPerfumistas
    {
        /// <summary>
        /// Endpoints da tabela de perfumistas
        /// </summary>
        /// <param name="app">Recebe como parâmetro a estrutura central da aplicação Web</param>
        public static void MapPerfumistasEndpoints(this IEndpointRouteBuilder app)
        {
            #region Consultas
            app.MapGet("/perfumistas", PerfumistasHandlers.ObterTodos).WithOpenApi();

            app.MapGet("/perfumistas/{id}", PerfumistasHandlers.BuscaPorId).WithOpenApi();

            app.MapGet("/perfumistasEFFunctions/porNome/{nome}", PerfumistasHandlers.BuscaPorNome).WithOpenApi();
            #endregion

            #region Inserções, modificações e deleções
            app.MapPost("/perfumistas", PerfumistasHandlers.InserePerfumista).WithOpenApi();

            app.MapPut("/perfumistas", PerfumistasHandlers.ModificaPerfumista).WithOpenApi();

            app.MapDelete("/perfumistas", PerfumistasHandlers.RemovePerfumistaPorId).WithOpenApi();
            #endregion
        }
    }
}
