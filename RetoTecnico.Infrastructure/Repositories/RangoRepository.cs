using Microsoft.EntityFrameworkCore;
using RetoTecnico.Application.Interfaces;
using RetoTecnico.Domain.Entities;
using RetoTecnico.Infrastructure.Persistence;

namespace RetoTecnico.Infrastructure.Repositories
{
    public class RangoRepository : IRangoRepository
    {
        private readonly AppDbContext _context;
        public RangoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<RangoImc>> GetAllAsync() => await _context.RangosImc.ToListAsync();

        public async Task<RangoImc?> GetByIdAsync(int id) => await _context.RangosImc.FindAsync(id);

        public async Task AddAsync(RangoImc rango)
        {
            await _context.RangosImc.AddAsync(rango);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RangoImc rango)
        {
            _context.RangosImc.Update(rango);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rango = await _context.RangosImc.FindAsync(id);
            if (rango != null)
            {
                _context.RangosImc.Remove(rango);
                await _context.SaveChangesAsync();
            }
        }
    }
}