using BookManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAuthors()
    {
        try
        {
            return Ok(await authorRepository.GetAuthors());
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        try
        {
            var result = await authorRepository.GetAuthor(id);

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
    public async Task<ActionResult<Author>> AddAuthor(Author author)
    {
        try
        {
            if(author == null) return BadRequest();

            var newAuthor = await authorRepository.AddAuthor(author);

            return CreatedAtAction(nameof(AddAuthor),
                new { id = newAuthor.Id }, newAuthor);
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error adding new author");
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Author>> EditAuthor(int id, Author author)
    {
        try
        {
            if(id != author.Id) return BadRequest("Author Id mismatch");

            var authorToEdit = await authorRepository.GetAuthor(id);

            if(authorToEdit == null)
            {
                return NotFound($"Author with Id = {id} not found");
            }

            return await authorRepository.EditAuthor(author);
        }
        catch (Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error updating author");
        }
        
    }
}