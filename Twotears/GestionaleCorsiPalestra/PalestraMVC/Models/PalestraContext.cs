using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PalestraMVC.Models;

public partial class PalestraContext : DbContext
{
    public PalestraContext()
    {
    }

    public PalestraContext(DbContextOptions<PalestraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Corso> Corsos { get; set; }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=Palestra;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Corso>(entity =>
        {
            entity.HasKey(e => e.Nome).HasName("PK__Corso__6F71C0DD6F4C29BB");

            entity.ToTable("Corso");

            entity.HasIndex(e => e.Codice, "UQ__Corso__40F9C18BB64B3A54").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Codice)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("codice");
            entity.Property(e => e.DataCorso).HasColumnName("data_corso");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descrizione");
            entity.Property(e => e.NPosti).HasColumnName("n_posti");
            entity.Property(e => e.OraFine)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ora_fine");
            entity.Property(e => e.OraInizio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ora_inizio");
            entity.Property(e => e.TipoCorso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_corso");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Utente__F3DBC5734FDA96EB");

            entity.ToTable("Utente");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.PasswordUtente)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_utente");

            entity.HasMany(d => d.NomeCorsos).WithMany(p => p.UsernameUtentes)
                .UsingEntity<Dictionary<string, object>>(
                    "Prenotum",
                    r => r.HasOne<Corso>().WithMany()
                        .HasForeignKey("NomeCorso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prenota__nome_co__3E52440B"),
                    l => l.HasOne<Utente>().WithMany()
                        .HasForeignKey("UsernameUtente")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Prenota__usernam__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("UsernameUtente", "NomeCorso").HasName("PK__Prenota__9E41E1636D98B229");
                        j.ToTable("Prenota");
                        j.IndexerProperty<string>("UsernameUtente")
                            .HasMaxLength(255)
                            .IsUnicode(false)
                            .HasColumnName("username_utente");
                        j.IndexerProperty<string>("NomeCorso")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("nome_corso");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
