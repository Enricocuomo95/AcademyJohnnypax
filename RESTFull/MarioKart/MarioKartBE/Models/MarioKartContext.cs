using System;
using Microsoft.EntityFrameworkCore;

namespace MarioKart.Models;

public partial class MarioKartContext : DbContext
{
    public MarioKartContext()
    {
    }

    public MarioKartContext(DbContextOptions<MarioKartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Giocatore> giocatori { get; set; }

    public virtual DbSet<Personaggio> personaggi { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=MarioKart;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Giocatore>(entity =>
        {
            entity.HasKey(e => e.GiocatoreID).HasName("PK__Giocator__068384CA3EE32765");

            entity.ToTable("Giocatore");

            entity.HasIndex(e => e.Username, "UQ__Giocator__536C85E47D3FC96B").IsUnique();

            entity.Property(e => e.Nominativo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Passward)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Personaggio>(entity =>
        {
            entity.HasKey(e => e.PersonaggioID).HasName("PK__Personag__D92EC22F0538D504");

            entity.ToTable("Personaggio");

            entity.HasIndex(e => e.Nome, "UQ__Personag__7D8FE3B2B14B61EC").IsUnique();

            entity.Property(e => e.Categoria)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Costo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Disponibile).HasDefaultValue(true);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Giocatore).WithMany(p => p.Squadra)
                .HasForeignKey(d => d.GiocatoreRIF)
                .HasConstraintName("FK__Personagg__Gioca__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
