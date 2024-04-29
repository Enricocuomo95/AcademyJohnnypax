using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Abbigliamento.Models;

public partial class NegozioAbbigliamentoContext : DbContext
{
    public NegozioAbbigliamentoContext()
    {
    }

    public NegozioAbbigliamentoContext(DbContextOptions<NegozioAbbigliamentoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Offertum> Offerta { get; set; }

    public virtual DbSet<Ordine> Ordines { get; set; }

    public virtual DbSet<Prodotto> Prodottos { get; set; }

    public virtual DbSet<Utente> Utentes { get; set; }

    public virtual DbSet<Variazione> Variaziones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=NegozioAbbigliamento;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Offertum>(entity =>
        {
            entity.HasKey(e => e.IdOfferta).HasName("PK__Offerta__C55B5ED90800BEFB");

            entity.Property(e => e.IdOfferta).HasColumnName("idOfferta");
            entity.Property(e => e.DataFine).HasColumnName("dataFine");
            entity.Property(e => e.DataInizio).HasColumnName("dataInizio");
            entity.Property(e => e.PrezzoOfferta).HasColumnName("prezzoOfferta");
            entity.Property(e => e.VariazioneRif).HasColumnName("variazioneRif");

            entity.HasOne(d => d.VariazioneRifNavigation).WithMany(p => p.Offerta)
                .HasForeignKey(d => d.VariazioneRif)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offerta__variazi__46E78A0C");
        });

        modelBuilder.Entity<Ordine>(entity =>
        {
            entity.HasKey(e => e.IdOrdine).HasName("PK__Ordine__569A3E9104932A89");

            entity.ToTable("Ordine");

            entity.Property(e => e.IdOrdine).HasColumnName("idOrdine");
            entity.Property(e => e.DataConsegna).HasColumnName("dataConsegna");
            entity.Property(e => e.DataOrdine).HasColumnName("dataOrdine");
            entity.Property(e => e.UtenteRif).HasColumnName("utenteRif");

            entity.HasOne(d => d.UtenteRifNavigation).WithMany(p => p.Ordines)
                .HasForeignKey(d => d.UtenteRif)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ordine__utenteRi__3C69FB99");

            entity.HasMany(d => d.ProdottoRifs).WithMany(p => p.OrdineRifs)
                .UsingEntity<Dictionary<string, object>>(
                    "Riferisce",
                    r => r.HasOne<Prodotto>().WithMany()
                        .HasForeignKey("ProdottoRif")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Riferisce__prodo__403A8C7D"),
                    l => l.HasOne<Ordine>().WithMany()
                        .HasForeignKey("OrdineRif")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Riferisce__ordin__3F466844"),
                    j =>
                    {
                        j.HasKey("OrdineRif", "ProdottoRif").HasName("PK__Riferisc__28F403125257E531");
                        j.ToTable("Riferisce");
                        j.IndexerProperty<int>("OrdineRif").HasColumnName("ordineRif");
                        j.IndexerProperty<int>("ProdottoRif").HasColumnName("prodottoRif");
                    });
        });

        modelBuilder.Entity<Prodotto>(entity =>
        {
            entity.HasKey(e => e.IdProdotto).HasName("PK__Prodotto__0A9870D90A5D2306");

            entity.ToTable("Prodotto");

            entity.Property(e => e.IdProdotto).HasColumnName("idProdotto");
            entity.Property(e => e.CategoriaProdotto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoriaProdotto");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descrizione");
            entity.Property(e => e.Marca)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Prezzo).HasColumnName("prezzo");
            entity.Property(e => e.UrlImg)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("urlImg");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.IdUtente).HasName("PK__Utente__11EA834F967EAD6A");

            entity.ToTable("Utente");

            entity.HasIndex(e => e.EmailUtente, "UQ__Utente__C1B09E5FCE6C9051").IsUnique();

            entity.Property(e => e.IdUtente).HasColumnName("idUtente");
            entity.Property(e => e.EmailUtente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emailUtente");
            entity.Property(e => e.PasswordUtente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("passwordUtente");
        });

        modelBuilder.Entity<Variazione>(entity =>
        {
            entity.HasKey(e => e.IdVariazione).HasName("PK__Variazio__05D8F9942D38C462");

            entity.ToTable("Variazione");

            entity.Property(e => e.IdVariazione).HasColumnName("idVariazione");
            entity.Property(e => e.Colore)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("colore");
            entity.Property(e => e.ProdottoRif).HasColumnName("prodottoRif");
            entity.Property(e => e.Quantita)
                .HasDefaultValue(0)
                .HasColumnName("quantita");
            entity.Property(e => e.Taglia)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("taglia");

            entity.HasOne(d => d.ProdottoRifNavigation).WithMany(p => p.Variaziones)
                .HasForeignKey(d => d.ProdottoRif)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Variazion__prodo__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
