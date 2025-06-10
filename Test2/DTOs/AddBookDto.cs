namespace Test2.DTOs;

public class AddBookDto
{
    public int IdPublishingHouse { get; set; }
    public String Name { get; set; }
    public String City { get; set; }
    public String Country { get; set; }
    public List<int> Authors { get; set; }
    public List<int> Genres { get; set; }
}