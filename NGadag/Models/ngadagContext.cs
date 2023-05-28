using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NGadag.Models
{
    public partial class ngadagContext : DbContext
    {
        public ngadagContext()
        {
        }

        public ngadagContext(DbContextOptions<ngadagContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<AdPhoto> AdPhotos { get; set; } = null!;
        public virtual DbSet<Applications> Applications { get; set; } = null!;
        public virtual DbSet<Companyinfo> Companyinfos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=ngadag;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("ad");

                entity.HasIndex(e => e.Title, "Title_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);
            });

            modelBuilder.Entity<AdPhoto>(entity =>
            {
                entity.ToTable("ad_photo");

                entity.HasIndex(e => e.AdId, "FK_ad_idx");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .HasColumnName("photo");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdPhotos)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ad");
            });

            modelBuilder.Entity<Applications>(entity =>
            {
                entity.ToTable("Applications");

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Message).HasColumnType("mediumtext");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Phone).HasMaxLength(45);
            });

            modelBuilder.Entity<Companyinfo>(entity =>
            {
                entity.ToTable("companyinfo");

                entity.Property(e => e.InfoType).HasMaxLength(255);

                entity.Property(e => e.InfoValue).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.City).HasMaxLength(45);

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.Phone).HasMaxLength(45);

                entity.Property(e => e.Role).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
