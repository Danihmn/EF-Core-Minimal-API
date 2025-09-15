namespace Perfumes.WebAPI.Endpoints.Services
{
    /// <summary>
    /// Classe referente aos Handlers que trabalham quando
    /// os Endpoints de Perfumes são chamados
    /// </summary>
    public static class PerfumesServices
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
        public static void InserePerfume(Context context, Perfume perfume)
        {
            var perfumista = context.Perfumistas.Find(perfume.PerfumistaId);

            context.Perfumes.Add(perfume);
            context.SaveChanges();
        }

        public static void ModificaPerfume(Context context, int id, Perfume novoPerfume)
        {
            var perfume = context.Perfumes.Find(id);

            if (perfume is null) return;

            perfume.Marca = novoPerfume.Marca;
            perfume.Nome = novoPerfume.Nome;
            perfume.Tipo = novoPerfume.Tipo;
            perfume.Valor = novoPerfume.Valor;
            perfume.PerfumistaId = novoPerfume.PerfumistaId;
            context.SaveChanges();
        }

        public static IResult AlteraPerfumeComUpdate(Context context, PerfumeUpdate perfumeUpdate)
        {
            var perfume = context.Perfumes.Find(perfumeUpdate.Id);

            if (string.IsNullOrWhiteSpace(perfumeUpdate.Nome))
                return Results.BadRequest("Nome do perfume é obrigatório.");

            perfume.Marca = perfumeUpdate.Marca;
            perfume.Nome = perfumeUpdate.Nome;
            perfume.Tipo = perfumeUpdate.Tipo;
            perfume.Valor = perfumeUpdate.Valor;

            context.SaveChanges();

            return Results.Ok(perfume);
        }

        public static IResult AlteraPerfumeComExecuteUpdate(Context context, PerfumeUpdate perfumeUpdate)
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
        }

        public static void RemovePerfumePorId(Context context, int id){
            var perfumeParaExcluir = context.Perfumes.Find(id);

            if (perfumeParaExcluir is null) return;

            context.Perfumes.Remove(perfumeParaExcluir);
            context.SaveChanges();
        }
        #endregion
    }
}
