namespace BookManager.Models
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int Id);
        Task<Author> AddAuthor(Author author);
        Task<Author> EditAuthor(Author author); 
    }
}