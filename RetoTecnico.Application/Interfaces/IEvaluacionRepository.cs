using RetoTecnico.Domain.Entities;

namespace RetoTecnico.Application.Interfaces
{
    public interface IEvaluacionRepository
    {
        Task<IEnumerable<RangoImc>> ObtenerRangosParametrizadosAsync();
        Task GuardarEvaluacionAsync(EvaluacionImc evaluacion);
    }
}