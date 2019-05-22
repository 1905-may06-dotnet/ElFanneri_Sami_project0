using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Data.Model
{
    public partial class PizzaBoxDbContext : DbContext
    {
        public PizzaBoxDbContext()
        {
        }

        public PizzaBoxDbContext(DbContextOptions<PizzaBoxDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetail { get; set; }
        public virtual DbSet<PizzaCrust> PizzaCrust { get; set; }
        public virtual DbSet<PizzaDetails> PizzaDetails { get; set; }
        public virtual DbSet<PizzaSize> PizzaSize { get; set; }
        public virtual DbSet<RestaurantList> RestaurantList { get; set; }
        public virtual DbSet<ToppingDetails> ToppingDetails { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=samiutadbserver.database.windows.net;Database=PizzaBoxDB;user id=samelfanneri;Password=Hunter8137!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BAF0E0017A8");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderAddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserID__4E88ABD4");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK__OrdersDe__D3B9D30C16937A84");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderPizzaName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrdersDet__Order__5165187F");
            });

            modelBuilder.Entity<PizzaCrust>(entity =>
            {
                entity.HasKey(e => e.CrustId)
                    .HasName("PK__PizzaCru__D8C84C3538CAE894");

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.CrustName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PizzaDetails>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK__PizzaDet__0B6012FDDBC47D16");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.PizzaDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PizzaSize>(entity =>
            {
                entity.HasKey(e => e.SizeId)
                    .HasName("PK__PizzaSiz__83BD095ADCE178F9");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RestaurantList>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Restaura__E7FEA4775F2B036D");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LocationAddress)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LocationPhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ToppingDetails>(entity =>
            {
                entity.HasKey(e => e.ToppingId)
                    .HasName("PK__ToppingD__EE02CCE5367CEE66");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserDeta__1788CCACAE05BB0A");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserLoginId).HasColumnName("UserLoginID");

                entity.HasOne(d => d.UserLogin)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.UserLoginId)
                    .HasConstraintName("FK__UserDetai__UserL__4BAC3F29");
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => e.UserLoginId)
                    .HasName("PK__UserLogi__107D56AC0368002D");

                entity.Property(e => e.UserLoginId).HasColumnName("UserLoginID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserPwd)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
