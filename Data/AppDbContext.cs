using Microsoft.EntityFrameworkCore;
using Backend_Prueba_Tecnica.Models;

namespace Backend_Prueba_Tecnica.Data
{
    // Equivalente a la combinación de EntityManager y Repositories en Spring Boot
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Un DbSet representa una tabla en la base de datos (equivalente a un JpaRepository<User, Integer>)
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aquí puedes configurar detalles específicos de las tablas.
            modelBuilder.Entity<User>().ToTable("users"); // Supabase suele usar nombres de tabla en minúsculas por convención en postgres
        }
    }
}
