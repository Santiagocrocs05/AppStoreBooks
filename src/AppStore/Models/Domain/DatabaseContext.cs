using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Models.Domain;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Book>()
        .HasMany(x => x.categoryRelationList)
        .WithMany(x => x.bookRelationList)
        .UsingEntity<BookCategory>(
        j => j
        .HasOne(p => p.Category)
        .WithMany(p => p.BookCategoryRelationList)
        .HasForeignKey(pc => pc.CategoryId),
        j => j
        .HasOne(p => p.Book)
        .WithMany(p => p.BookCategoryRelationList)
        .HasForeignKey(p => p.BookId),
        j =>
        {
            j.HasKey(t => new { t.BookId, t.CategoryId });
        });

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }


}