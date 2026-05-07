namespace RetoTecnico.Domain.Entities
{
    public class EvaluacionImc
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal PesoKilogramos { get; set; }
        public decimal AlturaCentimetros { get; set; }
        public DateTime FechaNacimiento { get; set; }



        public decimal ValorImc { get; set; }
        public string DescripcionResultado { get; set; } = string.Empty;
        public DateTime FechaEvaluacion { get; set; }
    }
}