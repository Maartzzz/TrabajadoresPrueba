using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<VistaTrabajadores> VistaTrabajadores { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Provincia> Provincia { get; set; }    
        public DbSet<Distrito> Distrito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoDocumento).HasMaxLength(3);
                entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
                entity.Property(e => e.Nombres).HasMaxLength(50);
                entity.Property(e => e.Sexo).HasMaxLength(1);
                entity.Property(e => e.IdDepartamento);
                entity.Property(e => e.IdProvincia);
                entity.Property(e => e.IdDistrito);
            });
            modelBuilder.Entity<VistaTrabajadores>().HasNoKey().ToView(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trabajador>()
                .HasOne(t => t.Departamento)
                .WithMany(d => d.Trabajador)
                .HasForeignKey(t => t.IdDepartamento);
            
            modelBuilder.Entity<Trabajador>()
                .HasOne(t => t.Provincia)
                .WithMany(p => p.Trabajador)
                .HasForeignKey(t => t.IdProvincia);

            modelBuilder.Entity<Trabajador>()
                .HasOne(t => t.Distrito)
                .WithMany(d => d.Trabajador)
                .HasForeignKey(t => t.IdDistrito);
        }
    }
    

    
}
