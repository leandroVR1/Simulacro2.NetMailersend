using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simulacro2.Models;

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

        modelBuilder.Entity<Medico>()
            .HasOne(m => m.Especialidades)
            .WithMany(e => e.Medicos)
            .HasForeignKey(m => m.EspecialidadId);

        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Medicos)
            .WithMany(m => m.Citas)
            .HasForeignKey(c => c.MedicoId);

        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Pacientes)
            .WithMany(p => p.Citas)
            .HasForeignKey(c => c.PacienteId);

        modelBuilder.Entity<Tratamiento>()
            .HasOne(t => t.Citas)
            .WithMany(c => c.Tratamientos)
            .HasForeignKey(t => t.CitaId);
    }
}
