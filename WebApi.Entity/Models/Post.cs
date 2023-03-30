namespace WebApi.Entity.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public Post()
    {
        CreatedDate = DateTime.Now;
    }
}
