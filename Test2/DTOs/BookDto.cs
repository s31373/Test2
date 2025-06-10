namespace Test2.DTOs;

public class BookDto
{
    public int IdBook { get; set; }
    public String Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public PublishingHouseDto PublishingHouse { get; set; }
    public List<GenreDto> Genres { get; set; }
    public List<AuthorDto> Authors { get; set; }
}