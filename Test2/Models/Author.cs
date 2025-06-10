namespace Test2.Models;

public class Author
{
    public int IdAuthor { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; }
}