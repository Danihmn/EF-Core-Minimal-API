using Perfumes.WebAPI.Domain.Entidades;
using Perfumes.WebAPI.Repo.Contratos;

namespace Perfumes.WebAPI.EndpointsHandlers
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham na lógica do CRUD quando
    /// os Endpoints de Perfumes são chamados
    /// </summary>
    public static class PerfumesHandlers
    {
        #region Consultas
        public static async Task<List<Perfume>> ObterTodos (IPerfumeRepository perfumeRepository) =>
            await perfumeRepository.ObterTodos();

        public static async Task<List<Perfume>> BuscaPorId (IPerfumeRepository perfumeRepository, int id) =>
            await perfumeRepository.ObterPorId(id);

        public static async Task<List<Perfume>> BuscaPorNome (IPerfumeRepository perfumeRepository, string nome) =>
            await perfumeRepository.ObterPorNome(nome);
        #endregion

        #region Inserções, modificações e deleções
        public static void InserePerfume(IPerfumeRepository perfumeRepository, Perfume perfume)
        {
            perfumeRepository.Adicionar(perfume);
            perfumeRepository.SalvarAlteracoes();
        }

        public static void ModificaPerfume(IPerfumeRepository perfumeRepository, int id, Perfume novoPerfume)
        {
            perfumeRepository.Atualizar(novoPerfume);
            perfumeRepository.SalvarAlteracoes();
        }

        public static void RemovePerfume(IPerfumeRepository perfume, int id)
        {
            perfume.Deletar(id);
            perfume.SalvarAlteracoes();
        }
        #endregion
    }
}
