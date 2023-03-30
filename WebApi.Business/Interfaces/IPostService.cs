using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface IPostService
{
    public PostCountViewModel GetPosts(int skip, int take);
    public Post Create(Post CreatedPost);
    public PostViewModel UpdateRange(PostViewModel posts);
    public bool DeleteRange(List<int> ids);
}
