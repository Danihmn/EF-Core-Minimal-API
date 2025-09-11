namespace Perfumes.WebAPI.Entidades
{
    /// <summary>
    /// Record class para modificação de perfumes com uso de Patch nos endpoints de perfumes
    /// </summary>
    public record PerfumeUpdate
    {
        public required int Id { get; init; }
        public required string Marca { get; init; }
        public required string Nome { get; init; }
        public required string Tipo { get; init; }
        public required float Valor { get; init; }
    }
}
