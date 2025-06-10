using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using Test2.DTOs;
using Test2.Models;

namespace Test2.Services;

public class BookService: IBookService
{
    private readonly BooksDbContext _context;

    public BookService(BooksDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDto>> GetBooksAsync(DateTime? releaseDate = null)
    {
        var query = _context.Books.AsNoTracking().AsQueryable();
        if (releaseDate != null)
            query = query.Where(b => b.ReleaseDate == releaseDate);
        var books = await query.Include(a => a.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .Include(a => a.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Include(a => a.PublishingHouse)
            .OrderBy(o => o.ReleaseDate)
            .ToListAsync();
        return books.Select(b => new BookDto
        {
            IdBook = b.IdBook,
            Name = b.Name,
            ReleaseDate = b.ReleaseDate,
            PublishingHouse = new PublishingHouseDto
            {
                IdPublishingHouse = b.PublishingHouse.IdPublishingHouse,
                Name = b.PublishingHouse.Name,
                Country = b.PublishingHouse.Country,
                City = b.PublishingHouse.City,
            },
            Genres = b.BookGenres.Select(bg => new GenreDto
            {
                IdGenre = bg.Genre.IdGenre,
                Name = bg.Genre.Name,
            }).ToList(),
            Authors = b.BookAuthors.Select(ba => new AuthorDto
            {
                IdAuthor = ba.Author.IdAuthor,
                FirstName = ba.Author.FirstName,
                LastName = ba.Author.LastName,
            }).ToList()
        }).ToList();
    }
    
    public async Task AddBookAsync(AddBookDto dto)
    {
        var invalidGenres = dto.Genres
            .Except(await _context.Genres.Select(m => m.IdGenre).ToListAsync())
            .ToList();
        
        if (invalidGenres.Any())
            throw new ArgumentException($"Invalid genre: {string.Join(", ", invalidGenres)}");
        
        var invalidAuthors = dto.Authors
            .Except(await _context.Authors.Select(m => m.IdAuthor).ToListAsync())
            .ToList();
        
        if (invalidAuthors.Any())
            throw new ArgumentException($"Invalid author: {string.Join(", ", invalidAuthors)}");

        var publishingHouse = await _context.PublishingHouses.FindAsync(dto.IdPublishingHouse);
        if (publishingHouse == null)
        {
            publishingHouse = new PublishingHouse
            {
                Name = dto.Name,
                Country = dto.Country,
                City = dto.City,
            };
            await _context.PublishingHouses.AddAsync(publishingHouse);
        }
        var book = new Book
        {
            Name = dto.Name,
            ReleaseDate = DateTime.Now,
            PublishingHouse = publishingHouse,
            BookGenres = dto.Genres.Select(g => new BookGenre
            {
                IdGenre = g
            }).ToList(),
            BookAuthors = dto.Authors.Select(a => new BookAuthor
            {
                IdAuthor = a
            }).ToList()
        };
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }
}