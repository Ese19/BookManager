using BookManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    // seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // seed author table
        modelBuilder.Entity<Author>().HasData(
            new Author {
                Id = 1,
                Name = "J.K. Rowling"
            }
        );

        modelBuilder.Entity<Author>().HasData(
            new Author {
                Id = 2,
                Name = "George R.R. Martin"
            }
        );

        // seed book table
        modelBuilder.Entity<Book>().HasData(
            new Book {
                Id = 1,
                Name = "A Game of Thrones",
                AuthorId = 2
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book {
                Id = 2,
                Name = "A Clash of Kings",
                AuthorId = 2
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book {
                Id = 3,
                Name = "Harry Potter and The Sorcerer's Stone",
                AuthorId = 1
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book {
                Id = 4,
                Name = "Harry Potter and The Goblet of Fire",
                AuthorId = 1
            }
        );
    }
}