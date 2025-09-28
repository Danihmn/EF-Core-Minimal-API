using Perfumes.WebAPI.Domain.Entidades;
using Perfumes.WebAPI.Repo.Contratos;

namespace Perfumes.WebAPI.EndpointsHandlers
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham na lógica do CRUD quando
    /// os Endpoints de Perfumistas são chamados
    /// </summary>
    public static class PerfumistasHandlers
    {
        #region Consultas
        public static List<Perfumista> ObterTodos(IPerfumistaRepository perfumistaRepository) =>
            perfumistaRepository.ObterTodos();

        public static List<Perfumista> BuscaPorId(IPerfumistaRepository perfumistaRepository, int id) =>
            perfumistaRepository.ObterPorId(id);

        public static List<Perfumista> BuscaPorNome(IPerfumistaRepository perfumistaRepository, string nome) =>
            perfumistaRepository.ObterPorNome(nome);
        #endregion

        #region Inserções, modificações e deleções
        public static void InserePerfumista(IPerfumistaRepository perfumistaRepository, Perfumista perfumista)
        {
            perfumistaRepository.Adicionar(perfumista);
            perfumistaRepository.SalvarAlteracoes();
        }

        public static void ModificaPerfumista(IPerfumistaRepository perfumistaRepository, int id, Perfumista perfumistaNovo)
        {
            perfumistaRepository.Atualizar(perfumistaNovo, id);
            perfumistaRepository.SalvarAlteracoes();
        }

        public static void RemovePerfumistaPorId(IPerfumistaRepository perfumistaRepository, int id)
        {
            perfumistaRepository.Deletar(id);
            perfumistaRepository.SalvarAlteracoes();
        }
        #endregion
    }
}
