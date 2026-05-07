using Microsoft.EntityFrameworkCore;
using RetoTecnico.Application.Interfaces;
using RetoTecnico.Domain.Entities;
using RetoTecnico.Infrastructure.Persistence;

namespace RetoTecnico.Infrastructure.Repositories
{
    public class EvaluacionRepository : IEvaluacionRepository
    {
        private readonly AppDbContext _context;

        public EvaluacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RangoImc>> ObtenerRangosParametrizadosAsync()
        {
            return await _context.RangosImc.ToListAsync();
        }

        public async Task GuardarEvaluacionAsync(EvaluacionImc evaluacion)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Evaluaciones.AddAsync(evaluacion);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}