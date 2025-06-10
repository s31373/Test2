using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace MovieApi.Data;

public class BooksDbContext: DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<PublishingHouse> PublishingHouses { get; set; }
    
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(e =>
        {
            e.HasKey(a => a.IdBook);
            e.Property(a => a.Name).HasMaxLength(50).IsRequired();
            e.Property(a => a.ReleaseDate).IsRequired();
            e.HasOne(a => a.PublishingHouse).WithMany(b => b.Books).HasForeignKey(a => a.IdPublishingHouse);
        });
        modelBuilder.Entity<PublishingHouse>(e =>
        {
            e.HasKey(a => a.IdPublishingHouse);
            e.Property(a => a.Name).HasMaxLength(50).IsRequired();
            e.Property(a => a.Country).HasMaxLength(50).IsRequired();
            e.Property(a => a.City).HasMaxLength(50).IsRequired();
        });
        modelBuilder.Entity<Author>(e =>
        {
            e.HasKey(a => a.IdAuthor);
            e.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            e.Property(a => a.LastName).HasMaxLength(50).IsRequired();
        });
        modelBuilder.Entity<Genre>(e =>
        {
            e.HasKey(a => a.IdGenre);
            e.Property(a => a.Name).HasMaxLength(50).IsRequired();
        });
        modelBuilder.Entity<BookAuthor>(e =>
        {
            e.HasKey(a =>  new { a.IdBook, a.IdAuthor });
            e.HasOne(a => a.Book).WithMany(a => a.BookAuthors).HasForeignKey(a => a.IdBook);
            e.HasOne(a => a.Author).WithMany(a => a.BookAuthors).HasForeignKey(a => a.IdAuthor);
        });
        modelBuilder.Entity<BookGenre>(e =>
        {
            e.HasKey(a =>  new { a.IdBook, a.IdGenre });
            e.HasOne(a => a.Book).WithMany(a => a.BookGenres).HasForeignKey(a => a.IdBook);
            e.HasOne(a => a.Genre).WithMany(a => a.BookGenres).HasForeignKey(a => a.IdGenre);
        });
    }
}