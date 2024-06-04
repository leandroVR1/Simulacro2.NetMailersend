using Microsoft.EntityFrameworkCore;
using Simulacro2.Models;

namespace Simulacro2.Data
{
    // Clase que representa el contexto de base de datos, hereda de DbContext
    public class BaseContext : DbContext
    {
        // Constructor que recibe opciones de configuración para el contexto
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        // Definición de las tablas en la base de datos
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        // Configuración adicional del modelo de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación uno a muchos entre Cita y Medico
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Citas)
                .HasForeignKey(c => c.MedicoId);

            // Configuración de la relación uno a muchos entre Cita y Paciente
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.PacienteId);

            // Configuración de la relación uno a muchos entre Medico y Especialidad
            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.EspecialidadId);

            // Configuración de la relación uno a muchos entre Tratamiento y Cita
            modelBuilder.Entity<Tratamiento>()
                .HasOne(t => t.Cita)
                .WithMany(c => c.Tratamientos)
                .HasForeignKey(t => t.CitaId);

            // Configuración de mapeo para el Enum EstadoEnum en la tabla Medicos
            modelBuilder.Entity<Medico>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Especialidad>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Paciente>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Cita>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Tratamiento>()
                .Property(m => m.Estado)
                .HasConversion<string>();
        }
    }
}
