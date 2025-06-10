using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController: ControllerBase
{
    private readonly IBookService _bookService;
    
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetBooks([FromQuery] DateTime? releaseDate)
    {
        try
        {
            var books = await _bookService.GetBooksAsync(releaseDate);
            return Ok(books);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving books.");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<List<BookDto>>> AddBook([FromBody] AddBookDto book)
    {
        try
        {
            await _bookService.AddBookAsync(book);
            return Ok();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding book.");
        }
    }
}