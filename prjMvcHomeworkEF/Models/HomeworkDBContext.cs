using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjMvcHomeworkEF.Models
{
    public partial class HomeworkDBContext : DbContext
    {
        public HomeworkDBContext()
        {
        }

        public HomeworkDBContext(DbContextOptions<HomeworkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblFood02> TblFood02s { get; set; } = null!;
        public virtual DbSet<TblFoodOrder> TblFoodOrders { get; set; } = null!;
        public virtual DbSet<TblHero> TblHeroes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("TblCustomer");

                entity.Property(e => e.Money).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFood02>(entity =>
            {
                entity.ToTable("TblFood02");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Style).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFoodOrder>(entity =>
            {
                entity.ToTable("TblFoodOrder");

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.PaidDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblHero>(entity =>
            {
                entity.ToTable("TblHero");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
