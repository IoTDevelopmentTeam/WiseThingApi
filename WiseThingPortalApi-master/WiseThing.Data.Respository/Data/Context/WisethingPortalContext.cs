using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WiseThing.Data.Respository
{
    public partial class WisethingPortalContext : DbContext
    {
      
        public WisethingPortalContext(DbContextOptions<WisethingPortalContext> options)
            : base(options)
        {
        }

        internal virtual DbSet<Device> Devices { get; set; }
        internal virtual DbSet<User> Users { get; set; }
        internal virtual DbSet<Userdevice> Userdevices { get; set; }
        internal virtual DbSet<Usertype> Usertypes { get; set; }
        internal virtual DbSet<PaneDetail> PaneDetails { get; set; }
        internal virtual DbSet<ConfigDetail> ConfigDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device");

                entity.HasIndex(e => e.DeviceTagName, "DeviceTagName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DeviceTagName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DeviceName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DeviceUniqueIdentifier)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.InputBy)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.InputDate).HasColumnType("datetime");
                
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserName, "UserName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserType, "UserToUserType_idx");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.InputDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserToUserType");
            });

            modelBuilder.Entity<Userdevice>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DeviceId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("userdevice");

                entity.HasIndex(e => e.DeviceId, "DeviceToUserDevice_idx");
                entity.Property(e => e.InputDate).HasColumnType("datetime");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Userdevices)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DeviceToUserDevice");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userdevices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserToUserDevice");
            });

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.ToTable("usertype");

                entity.HasIndex(e => e.UserTypeName, "UserTypeName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<PaneDetail>(entity=>{
                entity.HasKey(e => new { e.PaneId })
                 .HasName("PRIMARY");
            });
            modelBuilder.Entity<ConfigDetail>(entity =>
            {
                entity.HasKey(e => new { e.Id })
                     .HasName("PRIMARY");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
