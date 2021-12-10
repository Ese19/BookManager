using System.ComponentModel.DataAnnotations;

namespace BookManager.Models;
public class Book
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}