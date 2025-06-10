using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs;

public class AddBookDto
{
    public int IdPublishingHouse { get; set; }
    [MaxLength(50)]
    public String Name { get; set; }
    [MaxLength(50)]
    public String City { get; set; }
    [MaxLength(50)]
    public String Country { get; set; }
    public List<int> Authors { get; set; }
    public List<int> Genres { get; set; }
}