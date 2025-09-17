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
            #region Consultas
            app.MapGet("/perfumistas", PerfumistasHandlers.ObterTodos).WithOpenApi();

            app.MapGet("/perfumistas/primeiroPerfumista", PerfumistasHandlers.BuscaPrimeiro).WithOpenApi();

            app.MapGet("/perfumistas/ultimoPerfumista", PerfumistasHandlers.BuscaUltimo).WithOpenApi();

            app.MapGet("/perfumistas/{id}", PerfumistasHandlers.BuscaPorId).WithOpenApi();

            app.MapGet("/perfumistas/nomePerfumista{id}", PerfumistasHandlers.BuscaNomePorId).WithOpenApi();

            app.MapGet("/perfumistasEFFunctions/porNome/{nome}", PerfumistasHandlers.BuscaNomeComEFFunctions).WithOpenApi();

            app.MapGet("/perfumistas/retornaDefault/{nome}", PerfumistasHandlers.RetornaDefault).WithOpenApi();
            #endregion

            #region Inserções, modificações e deleções
            app.MapPost("/perfumistas", PerfumistasHandlers.InserePerfumista).WithOpenApi();

            app.MapPut("/perfumistas", PerfumistasHandlers.ModificaPerfumista).WithOpenApi();

            app.MapDelete("/perfumistas", PerfumistasHandlers.RemovePerfumistaPorId).WithOpenApi();
            #endregion
        }
    }
}
