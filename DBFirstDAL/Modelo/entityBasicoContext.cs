using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBFirstDAL.Modelo
{
    public partial class entityBasicoContext : DbContext
    {
        public entityBasicoContext()
        {
        }

        public entityBasicoContext(DbContextOptions<entityBasicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<NivelAcceso> NivelAccesos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=entityBasico;User Id=postgres;Password=pr0f3s0r");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("empleados");

                entity.HasIndex(e => e.NivelAccId, "IX_empleados_nivel_accId");

                entity.Property(e => e.NivelAccId).HasColumnName("nivel_accId");

                entity.Property(e => e.NombreEmpleado).HasColumnName("nombre_empleado");

                entity.HasOne(d => d.NivelAcc)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.NivelAccId);
            });

            modelBuilder.Entity<NivelAcceso>(entity =>
            {
                entity.ToTable("nivel_accesos");

                entity.Property(e => e.DescAcceso)
                    .HasColumnName("desc_acceso")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.NivelAcceso1).HasColumnName("nivel_acceso");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
