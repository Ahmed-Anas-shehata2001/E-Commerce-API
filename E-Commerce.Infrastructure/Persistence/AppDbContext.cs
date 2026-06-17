using E_Commerce.Domain.Features.CategoryFeature.Entities;
using E_Commerce.Domain.Features.ProductFeature.Entites;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        // model configuration 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure tables 



            // ******************************** PRODUCT ENTITY CONFIGURATION ********************************
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(p => p.RowVersion).IsRowVersion(); // for concurrency control
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);


                // configure RowVersion for concurrency control
                entity.Property(p => p.RowVersion).IsRowVersion().IsConcurrencyToken();

                // CreatedAt = timestamp automatically set by database when record is first inserted
                // It NEVER changes after creation
                // Used for auditing (when entity was created)
                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")         // -- DEFAULT GETUTCDATE() --  in SQL.
                    .ValueGeneratedOnAdd();

            
                entity.Property(p => p.UpdatedAt);    // BUT EF Core does NOT update it automatically in real life   so  // Best practice is to update it in SaveChanges override  or in domain as I did 


            });



            // ******************************** CATEGORY ENTITY CONFIGURATION ********************************
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                // configure RowVersion for concurrency control
                entity.Property(c => c.RowVersion).IsRowVersion().IsConcurrencyToken();

                // CreatedAt = timestamp automatically set by database when record is first inserted
                entity.Property(c => c.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")   // Database-generated OR BaseEntity default 
                    .ValueGeneratedOnAdd();

                // UpdatedAt = timestamp of last modification
                entity.Property(c => c.UpdatedAt);      // We  update it in SaveChanges override or in domain as  I did.  you can delete this line.
            });



        }

    }

}