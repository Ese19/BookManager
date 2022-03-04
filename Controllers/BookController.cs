using BookManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BookController : ControllerBase
{
    private readonly IBookRepository bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetBooks()
    {
        try
        {
             return Ok(await bookRepository.GetBooks());
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        try
        {
            var result = await bookRepository.GetBook(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
        
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        try
        {
            if(book == null) return BadRequest();

            var newBook = await bookRepository.AddBook(book);

            return CreatedAtAction(nameof(AddBook),
                new { id = newBook.Id }, newBook);
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error adding new book");
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Book>> EditBook(int id, Book book)
    {
        try
        {
            if(id != book.Id) return BadRequest("Book Id mismatch");

            var bookToEdit = await bookRepository.GetBook(id);

            if(bookToEdit == null)
            {
                return NotFound($"Book with Id = {id} not found");
            }

            return await bookRepository.EditBook(book);
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error updating book");
        }
        
    }
}