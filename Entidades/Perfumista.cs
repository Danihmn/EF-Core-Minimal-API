namespace Perfumes.WebAPI.Entidades
{
    /// <summary>
    /// Entidade de perfumistas
    /// </summary>
    public class Perfumista
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public ICollection<Perfume>? Perfumes { get; set; }
    }
}
