using BookManager.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Models;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext appDbContext;
    public AuthorRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;   
    }

    public async Task<Author> AddAuthor(Author author)
    {
        var result = await appDbContext.Authors.AddAsync(author);
        await appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Author> EditAuthor(Author author)
    {
         var result = await appDbContext.Authors
                .FirstOrDefaultAsync(e => e.Id == author.Id);

        if(result != null)
        {
            result.Name = author.Name;

            await appDbContext.SaveChangesAsync();

            return result;
        }
        
        return null;
    }

    public async Task<Author> GetAuthor(int Id)
    {
        return await appDbContext.Authors
            .Include(e => e.Books)
            .FirstOrDefaultAsync(e => e.Id == Id);
    }

    public async Task<IEnumerable<Author>> GetAuthors()
    {
        return await appDbContext.Authors.ToListAsync();
    }
}