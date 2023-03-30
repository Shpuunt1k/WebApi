using Microsoft.AspNetCore.Mvc;
using WebApi.Entity.AppDbContext;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Business.Interfaces;
using WebApi.Entity.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using WebApi.Entity.Models;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[SwaggerTag("Новости")]
[EnableCors]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
public class PostController : Controller
{
    private readonly IPostService _postService;
    public PostController(IPostService postservice)
    {
        _postService = postservice;
    }
    [AllowAnonymous]
    [HttpGet("getPosts")]
    public PostCountViewModel GetPosts(int skip = 0, int take = 50)
    {
        return _postService.GetPosts(skip, take);
    }
    [HttpPost("createPost")]
    public Post Create(Post post)
    {
        return _postService.Create(post);
    }
    [HttpPut("updateRange")]
    public PostViewModel UpdateRange(PostViewModel posts)
    {
        return _postService.UpdateRange(posts);
    }
    [HttpDelete("deleteRange")]
    public bool DeleteRange(List<int> ids)
    {
        return _postService.DeleteRange(ids);
    }
}
