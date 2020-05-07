using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Guia> Guias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GuiaStatus> GuiaStatus { get; set; }
        public DbSet<GuiaTipo> GuiaTipos { get; set; }
        public DbSet<StatusCheckIn> GuiaStatusCheckIns { get; set; }
    }
}