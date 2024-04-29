using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace aspWeb.Models;

public partial class ImpiegatoContext : DbContext
{
    public ImpiegatoContext()
    {
    }

    public ImpiegatoContext(DbContextOptions<ImpiegatoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cittum> Citta { get; set; }

    public virtual DbSet<Impiegato> Impiegatos { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Reparto> Repartos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=Impiegato;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cittum>(entity =>
        {
            entity.HasKey(e => e.CittaId).HasName("PK__Citta__F854DBA9C20BA931");

            entity.Property(e => e.CittaId).HasColumnName("citta_id");
            entity.Property(e => e.NomeCitta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_citta");
            entity.Property(e => e.ProvinciaRif).HasColumnName("provincia_rif");

            entity.HasOne(d => d.ProvinciaRifNavigation).WithMany(p => p.Citta)
                .HasForeignKey(d => d.ProvinciaRif)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citta__provincia__5165187F");
        });

        modelBuilder.Entity<Impiegato>(entity =>
        {
            entity.HasKey(e => e.ImpiegatoId).HasName("PK__Impiegat__4D99757FF0500BD1");

            entity.ToTable("Impiegato");

            entity.HasIndex(e => e.Matricola, "UQ__Impiegat__2C2751BAF58CFF57").IsUnique();

            entity.Property(e => e.ImpiegatoId).HasColumnName("impiegato_id");
            entity.Property(e => e.CittaRes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("citta_res");
            entity.Property(e => e.Cognome)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cognome");
            entity.Property(e => e.DataNascita).HasColumnName("data_nascita");
            entity.Property(e => e.IndirizzoRes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("indirizzo_res");
            entity.Property(e => e.Matricola)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("matricola");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.ProvinciaRes)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("provincia_res");
            entity.Property(e => e.Reparto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reparto");
            entity.Property(e => e.Ruolo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ruolo");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.ProvinciaId).HasName("PK__Provinci__9A2B25FFCB6607E6");

            entity.Property(e => e.ProvinciaId).HasColumnName("provincia_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Sigla)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sigla");
        });

        modelBuilder.Entity<Reparto>(entity =>
        {
            entity.HasKey(e => e.RepartoId).HasName("PK__Reparto__F6B5E4AC87D6B8DA");

            entity.ToTable("Reparto");

            entity.Property(e => e.RepartoId).HasColumnName("reparto_id");
            entity.Property(e => e.NomeReparto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_reparto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
