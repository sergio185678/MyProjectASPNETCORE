using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyProjectAPINETCore.Models;

public partial class DatabaseExampleContext : DbContext
{
    public DatabaseExampleContext()
    {
    }

    public DatabaseExampleContext(DbContextOptions<DatabaseExampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__cargo__982828C4E9F7F145");

            entity.ToTable("cargo");

            entity.Property(e => e.CargoId).HasColumnName("cargo_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__document__5D2EE7E53CCE971C");

            entity.ToTable("documento");

            entity.Property(e => e.IdDocumento).HasColumnName("id_documento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ruta)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ruta");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");

            entity.HasOne(d => d.oUsuario).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("id_usuario_fk");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__usuario__B9BE370FF7FA7ECB");

            entity.ToTable("usuario");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdCargo).HasColumnName("id_cargo");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");

            entity.HasOne(d => d.oCargo).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK_usuario_cargo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
