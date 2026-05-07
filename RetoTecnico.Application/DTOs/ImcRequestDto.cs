namespace RetoTecnico.Application.DTOs
{
    public class ImcRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal PesoKilogramos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal AlturaCentimetros { get; set; }
    }
}