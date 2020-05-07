using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Guia> Guias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GuiaStatus> GuiaStatus { get; set; }
        public DbSet<GuiaTipo> GuiaTipos { get; set; }
        public DbSet<StatusCheckIn> GuiaStatusCheckIns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AtendimentoSemPapel");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Guia>()
                .Property(g => g.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}