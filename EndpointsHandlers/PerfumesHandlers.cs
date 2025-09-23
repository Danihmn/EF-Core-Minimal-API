namespace Perfumes.WebAPI.EndpointsHandlers
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham na lógica do CRUD quando
    /// os Endpoints de Perfumes são chamados
    /// </summary>
    public static class PerfumesHandlers
    {
        #region Consultas
        public static List<Perfume> ObterTodos(Context context) =>
            context.Perfumes
                .Include(p => p.Perfumista)
                .OrderBy(perfume => perfume.Nome)
                .ThenBy(perfume => perfume.Valor)
                .ToList();

        public static Perfume? PrimeiroPerfume(Context context) =>
            context.Perfumes
                .Include(p => p.Perfumista)
                .FirstOrDefault();

        public static Perfume? UltimoPerfume(Context context) =>
            context.Perfumes
                .Include(p => p.Perfumista)
                .OrderByDescending(perfume => perfume.Nome)
                .FirstOrDefault();

        public static List<Perfume> BuscaPorId(Context context, int id) =>
            context.Perfumes
                .Where(perfume => perfume.Id == id)
                .Include(perfumista => perfumista.Perfumista)
                .ToList();

        public static List<String> BuscaNomePorId(Context context, int id) =>
            context.Perfumes
                .Where(perfume => perfume.Id == id)
                .Include(perfumista => perfumista.Perfumista)
                .Select(perfume => perfume.Nome)
                .ToList();

        public static List<Perfume> BuscaNomeComEFFunctions(Context context, string nome) =>
            // Utiliza função Like do EF para localizar os dados, sem a necessidade das strings terem que ser idênticas
            // Nos parâmetros da função Like, os % dizem que deve ser localizado tudo aquilo que vem antes e depois do que foi digitado
            context.Perfumes
                .Where(perfume => EF.Functions
                .Like(perfume.Nome, $"%{nome}%"))
                .Include(perfumista => perfumista.Perfumista).ToList();

        public static List<Perfume> BuscaNomeComLinQ(Context context, string nome) =>
            // Utiliza função Like Contains para localizar o perfume com base no nome escrito
            context.Perfumes
                .Where(perfume => perfume.Nome
                .Contains(nome))
                .Include(perfumista => perfumista.Perfumista)
                .ToList();

        public static Perfume RetornaDefault(Context context, string nome) =>
            // Caso o perfume não for encontrado na consulta, será retornado um perfume Default, que é instanciado
            context.Perfumes
                .Include(perfumista => perfumista.Perfumista)
                .FirstOrDefault(perfume => perfume.Nome.Contains(nome)) ??
                new Perfume
                {
                    Id = 404,
                    Marca = "Emporio Armani",
                    Nome = "Stronger With You",
                    Tipo = "Eau de Parfum",
                    Valor = 800,
                    PerfumistaId = 2,
                    Perfumista = new Perfumista { Id = 2, Nome = "Alberto Morillas" }
                };
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
