using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductsMinimalAPI_1165395.Models;

public partial class ProductsBcbs1165395Context : DbContext
{
    public ProductsBcbs1165395Context()
    {
    }

    public ProductsBcbs1165395Context(DbContextOptions<ProductsBcbs1165395Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=Teja;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;database=ProductsBCBS_1165395;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pcolor).HasColumnName("PColor");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.PcolorNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Pcolor)
                .HasConstraintName("FK_Products_ProductColors");

            entity.HasMany(d => d.Suppliers).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductSupplier",
                    r => r.HasOne<Supplier>().WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductSuppliers_Supplier"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductSuppliers_Products"),
                    j =>
                    {
                        j.HasKey("ProductId", "SupplierId");
                    });
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.ColourId);

            entity.Property(e => e.ColourId).ValueGeneratedNever();
            entity.Property(e => e.ColourName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).ValueGeneratedNever();
            entity.Property(e => e.SupplierAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
