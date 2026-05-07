using RetoTecnico.Application.DTOs;
using RetoTecnico.Application.Interfaces;
using RetoTecnico.Domain.Entities;

namespace RetoTecnico.Application.Services
{

    public class ImcService
    {
        private readonly IEvaluacionRepository _repository;

        public ImcService(IEvaluacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ImcResponseDto> CalcularYGuardarImcAsync(ImcRequestDto request)
        {
            // Calcular la edad exacta
            var hoy = DateTime.Today;
            int edad = hoy.Year - request.FechaNacimiento.Year;
            if (request.FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;

            // Validación de edad 
            if (edad <= 19)
            {
                return new ImcResponseDto
                {
                    ValorImc = 0,
                    Descripcion = "No se han registrado los percentiles para niños y adolescentes" 
                };
            }

            // Conversión de unidades 
            decimal alturaMetros = request.AlturaCentimetros / 100m;
            decimal imcCalculado = request.PesoKilogramos / (alturaMetros * alturaMetros); 

            imcCalculado = Math.Round(imcCalculado, 1);

            // Obtener parametrización desde la BD
            var rangos = await _repository.ObtenerRangosParametrizadosAsync();
            string descripcionResultado = "No se encontró un rango parametrizado.";

            foreach (var rango in rangos)
            {
                bool cumpleMinimo = !rango.ValorMinimo.HasValue || imcCalculado >= rango.ValorMinimo.Value;
                bool cumpleMaximo = !rango.ValorMaximo.HasValue || imcCalculado <= rango.ValorMaximo.Value;

                if (cumpleMinimo && cumpleMaximo)
                {
                    descripcionResultado = rango.Descripcion;
                    break;
                }
            }

            var nuevaEvaluacion = new EvaluacionImc
            {
                Nombre = request.Nombre,
                PesoKilogramos = request.PesoKilogramos,
                AlturaCentimetros = request.AlturaCentimetros,
                FechaNacimiento = request.FechaNacimiento,
                ValorImc = imcCalculado,
                DescripcionResultado = descripcionResultado,
                FechaEvaluacion = DateTime.Now
            };

            await _repository.GuardarEvaluacionAsync(nuevaEvaluacion);

            return new ImcResponseDto
            {
                ValorImc = imcCalculado,
                Descripcion = descripcionResultado
            };
        }
    }
}