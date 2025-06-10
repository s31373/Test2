namespace Test2.Models;

public class Book
{
    public int IdBook { get; set; }
    public String Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public PublishingHouse PublishingHouse { get; set; }
    public int IdPublishingHouse { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; }
    public ICollection<BookGenre> BookGenres { get; set; }
}