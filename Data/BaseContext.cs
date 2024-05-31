using Microsoft.EntityFrameworkCore;
using Simulacro2.Models;

namespace Simulacro2.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Citas)
                .HasForeignKey(c => c.MedicoId);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.EspecialidadId);

            // Configuración de mapeo para el Enum EstadoEnum en la tabla Medicos
            modelBuilder.Entity<Medico>()
                .Property(m => m.Estado)
                .HasConversion<string>();
        }
    }
}
