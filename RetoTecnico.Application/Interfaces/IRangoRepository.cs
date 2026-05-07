using RetoTecnico.Domain.Entities;

namespace RetoTecnico.Application.Interfaces
{
    public interface IRangoRepository
    {
        Task<IEnumerable<RangoImc>> GetAllAsync();
        Task<RangoImc?> GetByIdAsync(int id);
        Task AddAsync(RangoImc rango);
        Task UpdateAsync(RangoImc rango);
        Task DeleteAsync(int id);
    }
}