namespace Perfumes.WebAPI.EndpointsHandlers
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham na lógica do CRUD quando
    /// os Endpoints de Perfumes são chamados
    /// </summary>
    public static class PerfumesHandlers
    {
        #region Consultas
        public static List<Perfume> ObterTodos(IPerfumeRepository perfumeRepository) =>
            perfumeRepository.ObterTodos();

        public static List<Perfume> BuscaPorId(IPerfumeRepository perfumeRepository, int id) =>
            perfumeRepository.ObterPorId(id);

        public static List<Perfume> BuscaPorNome(IPerfumeRepository perfumeRepository, string nome) =>
            perfumeRepository.ObterPorNome(nome);
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
