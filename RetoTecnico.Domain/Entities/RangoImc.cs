namespace RetoTecnico.Domain.Entities
{
    public class RangoImc
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty; 
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
    }
}