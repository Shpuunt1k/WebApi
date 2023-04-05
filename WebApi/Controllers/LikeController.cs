using Microsoft.AspNetCore.Mvc;
using WebApi.Entity.AppDbContext;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Business.Interfaces;
using WebApi.Entity.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Entity.Models;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[SwaggerTag("Избранное")]
[EnableCors]
public class LikeController : Controller
{

    private readonly ILikeService _likeService;
    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpGet("getLikes")]
    public LikeViewModel GetLikes(int skip = 0, int take = 50)
    {
        return _likeService.GetLikes(skip, take);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPost("setLike")]
    public SetLikeViewModel SetLike(SetLikeViewModel like)
    {
        return _likeService.SetLike(like);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpDelete("deleteLike")]
    public bool DeleteLike(List<int> ids)
    {
        return _likeService.DeleteLike(ids);
    }
}
