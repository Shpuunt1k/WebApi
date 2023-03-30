using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class PostViewModel
{
    public List<Post> Posts { get; set; }
}
public class PostCountViewModel : PostViewModel
{
    public int TotalCount { get; set; }
}
