using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.CartFeature.Entities;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;
using E_Commerce.Domain.Features.WishlistFeature.Entities;
using E_Commerce.Infrastructure.Identity;
using E_Commerce.Infrastructure.Identity.Identity_Entites;
using E_Commerce.Infrastructure.Identity.Identity_Entites.UserSecurityEvents;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
namespace E_Commerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ICurrentUserService _currentUser;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUser) : base(options)
        {
            _currentUser = currentUser;

        }

        public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<UserLoginHistory> UserLoginHistories => Set<UserLoginHistory>();
        public DbSet<UserSecurityEvent> UserSecurityEvents => Set<UserSecurityEvent>();
        public DbSet<UserTwoFactorMethod> UserTwoFactorMethods => Set<UserTwoFactorMethod>();


        // Catalog
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Review> Reviews => Set<Review>();


        // wishlist
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();

        // cart
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  //     // Configure Identity tables (AspNetUsers, AspNetRoles, etc.)
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(u => !u.IsDeleted);  // global query filter


            // to add these configurations outside    ( best practise )
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }

        public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId?.ToString() ?? "System";

            // Auditing
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(e => e.CreatedAtUtc).CurrentValue = DateTime.UtcNow;
                        entry.Property(e => e.CreatedBy).CurrentValue = userId;
                        break;

                    case EntityState.Modified:
                        entry.Property(e => e.UpdatedAtUtc).CurrentValue = DateTime.UtcNow;
                        entry.Property(e => e.UpdatedBy).CurrentValue = userId;
                        break;


                }
            }
            // Soft Delete
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.MarkAsDeleted(userId);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);



        }

    }





    // ***************************** Configuration ************************************ //




    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.RowVersion)
                .IsRowVersion();

            builder.HasIndex(c => c.Name)
                .IsUnique();


            builder.Property(c => c.Status)
             .HasConversion<string>();

            builder.HasQueryFilter(c => c.Status != CategoryStatus.Archived);

        }
    }


    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.SKU)
                .IsUnique();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Weight)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Status)
                .HasConversion<string>();

            builder.Property(p => p.RowVersion)
                .IsRowVersion();

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.Name);

            builder.HasIndex(p => p.CategoryId);

            builder.HasIndex(p => p.BrandId);

            builder.HasIndex(p => p.SellerId);

            builder.HasQueryFilter(p => p.Status != ProductStatus.Archived);
        }
    }


    public sealed class BrandConfiguration
        : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.Property(b => b.Status)
                .HasConversion<string>();

            builder.Property(b => b.RowVersion)
                .IsRowVersion();

            builder.HasIndex(b => b.Name)
                .IsUnique();

            builder.HasQueryFilter(b => b.Status != BrandStatus.Archived);
        }
    }


    public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.Property(r => r.RowVersion)
                .IsRowVersion();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => r.ProductId);

            builder.HasIndex(r => r.CustomerId);

        }
    }




    public sealed class WishlistConfiguration
        : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(x => x.UpdatedBy)
                   .HasMaxLength(100);



            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            builder.HasMany(x => x.Items)
                   .WithOne(x => x.Wishlist)
                   .HasForeignKey(x => x.WishlistId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One wishlist per customer
            builder.HasIndex(x => x.CustomerId)
                   .IsUnique();
        }
    }


    public sealed class WishlistItemConfiguration
    : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("WishlistItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(x => x.UpdatedBy)
                   .HasMaxLength(100);


            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            // Prevent duplicate products in the same wishlist
            builder.HasIndex(x => new
            {
                x.WishlistId,
                x.ProductId
            }).IsUnique();

            builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);


        }
    }

    public sealed class CartConfiguration
    : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(x => x.UpdatedBy)
                   .HasMaxLength(100);


            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            // Cart -> CartItems
            builder.HasMany(x => x.Items)
                   .WithOne(x => x.Cart)
                   .HasForeignKey(x => x.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One cart per customer
            builder.HasIndex(x => x.CustomerId)
                   .IsUnique();
        }
    }


    public sealed class CartItemConfiguration
    : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                   .IsRequired();

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .IsRequired();

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(100);

            builder.Property(x => x.UpdatedBy)
                   .HasMaxLength(100);


            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            // Prevent duplicate products in the same cart
            builder.HasIndex(x => new
            {
                x.CartId,
                x.ProductId
            }).IsUnique();

            // CartItem -> Product
            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ApplicationUserConfiguration
: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName)
                   .HasMaxLength(100);

            builder.Property(x => x.LastName)
                   .HasMaxLength(100);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }



    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TokenHash)
                   .IsRequired()
                   .HasMaxLength(512);

            builder.HasIndex(x => x.TokenHash)
                   .IsUnique();

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.ReplacedByToken)
           .WithMany()
           .HasForeignKey(x => x.ReplacedByTokenId)
           .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.User)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Session)
        .WithMany(x => x.RefreshTokens)
        .HasForeignKey(x => x.SessionId)
        .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.RevocationReason)
   .HasConversion<string>()
   .HasMaxLength(50);
        }
    }
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeviceName)
                   .HasMaxLength(100);

            builder.Property(x => x.UserAgent)
                   .HasMaxLength(500);

            builder.Property(x => x.IpAddress)
                   .HasMaxLength(50);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Sessions)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);




        }
    }

    public class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("UserLoginHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FailureReason)
                   .HasMaxLength(300);

            builder.Property(x => x.UserAgent)
                   .HasMaxLength(500);

            builder.Property(x => x.IpAddress)
                   .HasMaxLength(50);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.LoginAtUtc);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.LoginHistories)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class UserSecurityEventConfiguration : IEntityTypeConfiguration<UserSecurityEvent>
    {
        public void Configure(EntityTypeBuilder<UserSecurityEvent> builder)
        {
            builder.ToTable("UserSecurityEvents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Details)
                   .HasMaxLength(1000);

            builder.Property(x => x.IpAddress)
                   .HasMaxLength(50);

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.EventType);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.SecurityEvents)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // to show as string not a number 
            builder.Property(x => x.EventType)
          .HasConversion<string>()
          .HasMaxLength(100);
        }
    }

    public class UserTwoFactorMethodConfiguration : IEntityTypeConfiguration<UserTwoFactorMethod>
    {
        public void Configure(EntityTypeBuilder<UserTwoFactorMethod> builder)
        {
            builder.ToTable("UserTwoFactorMethods");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.MethodType })
                   .IsUnique();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.TwoFactorMethods)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }



}



