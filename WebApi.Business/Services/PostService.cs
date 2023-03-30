using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class PostService : IPostService
{
    private readonly IRepository<Post> _postRepository;
    public PostService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    public PostCountViewModel GetPosts(int skip, int take)
    {
        var posts = _postRepository.Get(skip, take);

        var result = new PostCountViewModel
        {
            Posts = new List<Post>(),
            TotalCount = _postRepository.GetAll().Count()
        };

        result.Posts = posts.Select(post => new Post
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            CreatedDate = post.CreatedDate
        }).ToList();

        return result;
    }
    public Post Create(Post CreatedPost)
    {
        var post = new Post
        {
            Id=CreatedPost.Id,
            Title = CreatedPost.Title,
            Description = CreatedPost.Description,
            CreatedDate= CreatedPost.CreatedDate,
        };
        var created = _postRepository.Create(post);
        return new Post
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            CreatedDate = created.CreatedDate
        };
    }
    public PostViewModel UpdateRange(PostViewModel posts)
    {
        var existingPosts = _postRepository.GetAll().ToList()
            .Where(pst => posts.Posts.Any(post => post.Id == pst.Id)).ToList();

        var updated = (from post in posts.Posts
                       where existingPosts.Any(pst => pst.Id == post.Id)
                       select new Post
                       {
                           Id = post.Id,
                           Title = post.Title,
                           Description = post.Description,
                           CreatedDate = post.CreatedDate,
                       }).ToList();

        _postRepository.UpdateRange(updated);

        var updatedPosts = _postRepository.GetAll().ToList()
            .Where(pst => posts.Posts.Any(post => post.Id == pst.Id)).ToList();
        return new PostViewModel
        {
            Posts = updatedPosts.Select(post => new Post
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CreatedDate = post.CreatedDate,
            }).ToList()
        };
    }
    public bool DeleteRange(List<int> ids)
    {
        var existingPosts = _postRepository.GetAll().ToList()
    .Where(p => ids.Any(id => id == p.Id)).ToList();

        return _postRepository.DeleteRange(existingPosts);
    }
}
