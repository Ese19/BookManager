using BookManager.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Models;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext appDbContext;
    public BookRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;

    }
    public async Task<Book> AddBook(Book book)
    {
        // Entity framework should ignore author body when adding new book
        if(book.Author != null)
        {
            appDbContext.Entry(book.Author).State = EntityState.Unchanged;
        }

        var result = await appDbContext.Books.AddAsync(book);
        await appDbContext.SaveChangesAsync();
        return result.Entity;

    }

    public async Task<Book> EditBook(Book book)
    {
        var result = await appDbContext.Books
                .FirstOrDefaultAsync(e => e.Id == book.Id);

        if(result != null)
        {
            result.Name = book.Name;
            result.AuthorId = book.AuthorId;
            if(book.AuthorId != 0)
            {
                result.AuthorId = book.AuthorId;
            }
            else if(book.Author != null)
            {
                result.AuthorId = book.Author.Id;
            }

            await appDbContext.SaveChangesAsync();

            return result;
        }
        
        return null;
    }

    public async Task<Book> GetBook(int Id)
    {
        return await appDbContext.Books
            .Include(e => e.Author)
            .FirstOrDefaultAsync(e => e.Id == Id);
 
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await appDbContext.Books.ToListAsync();
    }
}