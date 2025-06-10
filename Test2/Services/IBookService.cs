using Test2.DTOs;

namespace Test2.Services;

public interface IBookService
{
    Task<List<BookDto>> GetBooksAsync(DateTime? releaseDate = null);
    
    Task AddBookAsync(AddBookDto book);
}