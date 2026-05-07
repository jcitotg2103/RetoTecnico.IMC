using Microsoft.EntityFrameworkCore;
using RetoTecnico.Domain.Entities;

namespace RetoTecnico.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EvaluacionImc> Evaluaciones { get; set; }
        public DbSet<RangoImc> RangosImc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EvaluacionImc>()
                .Property(e => e.PesoKilogramos).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<EvaluacionImc>()
                .Property(e => e.AlturaCentimetros).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<EvaluacionImc>()
                .Property(e => e.ValorImc).HasColumnType("decimal(18,1)");

            modelBuilder.Entity<RangoImc>()
                .Property(r => r.ValorMinimo).HasColumnType("decimal(18,1)");
            modelBuilder.Entity<RangoImc>()
                .Property(r => r.ValorMaximo).HasColumnType("decimal(18,1)");
        }
    }
}