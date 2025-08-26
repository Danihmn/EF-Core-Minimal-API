namespace Perfumes.WebAPI.Entidades
{
    /// <summary>
    /// Entidade de perfumes
    /// </summary>
    public class Perfume
    {
        public int Id { get; set; }
        public required string Marca { get; set; }
        public required string Nome { get; set; }
        public required string Tipo { get; set; }
        public float Valor { get; set; }

        public int PerfumistaId { get; set; }

        public Perfumista? Perfumista { get; set; }
    }
}