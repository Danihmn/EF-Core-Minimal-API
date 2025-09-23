namespace Perfumes.WebAPI.EndpointsHandlers
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham na lógica do CRUD quando
    /// os Endpoints de Perfumistas são chamados
    /// </summary>
    public static class PerfumistasHandlers
    {
        #region Consultas
        public static List<Perfumista> ObterTodos(Context context) =>
            context.Perfumistas
                .Include(p => p.Perfumes)
                .OrderBy(perfumista => perfumista.Nome)
                .ToList();

        public static Perfumista? BuscaPrimeiro(Context context) =>
            context.Perfumistas
                .Include(perfume => perfume.Perfumes)
                .FirstOrDefault();

        public static Perfumista? BuscaUltimo(Context context) =>
            context.Perfumistas
                .Include(perfume => perfume.Perfumes)
                .OrderByDescending(perfumista => perfumista.Nome)
                .FirstOrDefault();

        public static List<Perfumista> BuscaPorId(Context context, int id) =>
            context.Perfumistas
                .Where(diretor => diretor.Id == id)
                .Include(perfumista => perfumista.Perfumes)
                .ToList();

        public static string BuscaNomePorId(Context context, int id) =>
            context.Perfumistas
                .Where(diretor => diretor.Id == id)
                .Include(perfumista => perfumista.Perfumes)
                .Select(perfumista => perfumista.Nome)
                .FirstOrDefault() ?? "Perfumista não encontrado";

        public static List<Perfumista> BuscaNomeComEFFunctions(Context context, string nome) =>
            // Utiliza função Like do EF para localizar os dados, sem a necessidade das strings terem que ser idênticas
            // Nos parâmetros da função Like, os % dizem que deve ser localizado tudo aquilo que vem antes e depois do que foi digitado
            context.Perfumistas
                .Where(perfumista => EF.Functions
                .Like(perfumista.Nome, $"%{nome}%"))
                .Include(perfume => perfume.Perfumes).ToList();

        public static Perfumista RetornaDefault(Context context, string nome) =>
            // Caso o perfumista não for encontrado na consulta, será retornado um perfumista Default, que é instanciado
            context.Perfumistas
                .Include(perfumista => perfumista.Perfumes)
                .FirstOrDefault(perfumista => perfumista.Nome.Contains(nome)) ??
                new Perfumista
                {
                    Id = 404,
                    Nome = "Giorgio Armani"
                };
        #endregion

        #region Inserções, modificações e deleções
        public static void InserePerfumista(Context context, Perfumista perfumista)
        {
            context.Add(perfumista);
            context.SaveChanges();
        }

        public static void ModificaPerfumista(Context context, int id, Perfumista perfumistaNovo)
        {
            var perfumista = context.Perfumistas.Find(id);

            if (perfumista != null)
            {
                perfumista.Nome = perfumistaNovo.Nome;
                perfumista.Perfumes = perfumistaNovo.Perfumes;

                context.SaveChanges();
            }
        }

        public static void RemovePerfumistaPorId(Context context, int id)
        {
            var perfumista = context.Perfumistas
                .Include(p => p.Perfumes)
                .FirstOrDefault(p => p.Id == id);

            if (perfumista != null)
            {
                // Remove os perfumes associados ao perfumista
                context.Perfumes.RemoveRange(perfumista.Perfumes);

                // Remove o perfumista
                context.Perfumistas.Remove(perfumista);

                context.SaveChanges();
            }
        }
        #endregion
    }
}
