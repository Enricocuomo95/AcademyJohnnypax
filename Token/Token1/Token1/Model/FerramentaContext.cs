using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Token1.Model;

public partial class FerramentaContext : DbContext
{
    public FerramentaContext()
    {
    }

    public FerramentaContext(DbContextOptions<FerramentaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Prodotto> Prodottos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=Ferramenta;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prodotto>(entity =>
        {
            entity.HasKey(e => e.Codice).HasName("PK__Prodotto__0636EC1C3ACDFEDC");

            entity.ToTable("Prodotto");

            entity.HasIndex(e => new { e.Nome, e.Categoria }, "UQ__Prodotto__CD0FF64A09CE44F4").IsUnique();

            entity.Property(e => e.Codice)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataCreazione).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
