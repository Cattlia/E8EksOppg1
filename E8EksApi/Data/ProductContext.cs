using Microsoft.EntityFrameworkCore;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

    public DbSet<Product> products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Name).HasColumnName("name");
            entity.Property(c => c.Brand).HasColumnName("brand");
            entity.Property(c => c.Price).HasColumnName("price");
            entity.Property(c => c.Stock).HasColumnName("stock");
        });
    }
}


