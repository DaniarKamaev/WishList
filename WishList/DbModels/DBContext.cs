using Microsoft.EntityFrameworkCore;
using WishList.DbModels;

namespace WishList.DbModels
{
    public partial class SysContext : DbContext
    {
        public SysContext()
        {
        }

        public SysContext(DbContextOptions<SysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();
                entity.Property(e => e.UserName).HasMaxLength(45);
                entity.Property(e => e.UserPassword).HasMaxLength(255);
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasKey(e => e.WishListId).HasName("PRIMARY");
                entity.ToTable("WishList");
                entity.HasIndex(e => e.UserId, "UserId");
                entity.Property(e => e.Booked).HasDefaultValueSql("'0'");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");
                entity.Property(e => e.Gift).HasMaxLength(255);
                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("WishList_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}