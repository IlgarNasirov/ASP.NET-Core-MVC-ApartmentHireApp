using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApartmentHireApp.Models;

public partial class ApartmentHireContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApartmentHireContext(DbContextOptions<ApartmentHireContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;

    }
    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.ToTable("apartment");

            entity.HasIndex(e => e.No, "UQ__apartmen__3213D081F82AD5FF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Blockid).HasColumnName("blockid");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.No)
                .HasMaxLength(50)
                .HasColumnName("no");
            entity.Property(e => e.Numberofrooms).HasColumnName("numberofrooms");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");

            entity.HasOne(d => d.Block).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Blockid)
                .HasConstraintName("FK_apartment_block");
        });

        modelBuilder.Entity<Block>(entity =>
        {
            entity.ToTable("block");

            entity.HasIndex(e => e.Name, "UQ__block__72E12F1B5AE25212").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.ToTable("contract");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.Apartmentid)
                .HasConstraintName("FK_contract_apartment");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
