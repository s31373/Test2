namespace Test2.Models;

public class Genre
{
    public int IdGenre { get; set; }
    public String Name { get; set; }
    public ICollection<BookGenre> BookGenres { get; set; }
}