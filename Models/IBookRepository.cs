namespace BookManager.Models
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int Id);
        Task<Book> AddBook(Book book);
        Task<Book> EditBook(Book book);

    }
}