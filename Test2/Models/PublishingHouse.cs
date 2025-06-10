namespace Test2.Models;

public class PublishingHouse
{
    public int IdPublishingHouse { get; set; }
    public String Name { get; set; }
    public String Country { get; set; }
    public String City { get; set; }
    public ICollection<Book> Books { get; set; }
}