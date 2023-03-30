using Microsoft.AspNetCore.Mvc;
using WebApi.Entity.AppDbContext;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Business.Interfaces;
using WebApi.Entity.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity.Models;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[SwaggerTag("Пользователи")]
[EnableCors]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private IUserProvider _userProvider { get; set; }
    private readonly int _userId;
    public UserController(IUserService userService, IJwtService jwtService, IUserProvider userProvider)
    {
        _userService = userService;
        _jwtService = jwtService;
        _userProvider = userProvider;
        _userId = _userProvider.GetUserId();
    }
    [AllowAnonymous]
    [HttpPost("token")]
    public IActionResult Token(UserLoginViewModel loginModel)
    {
        var response = _jwtService.Token(loginModel.Login, loginModel.Password);
        if (response == null)
        {
            return BadRequest("Invalid username or password.");
        }
        return Ok(response);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpGet("getUsers")]
    public UserViewModel GetUsers(int skip = 0, int take = 50)
    {
        return _userService.GetUsers(skip, take);
    }
    [HttpPost("createUser")]
    public UserItemViewModel Create(UserItemViewModel user)
    {
        return _userService.Create(user);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPut("updateRange")]
    public UpdateUserViewModel UpdateRange(UpdateUserViewModel users)
    {
        return _userService.UpdateRange(users);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpDelete("deleteRange")]
    public bool DeleteRange(List<int> ids)
    {
        return _userService.DeleteRange(ids);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpGet("getUserId")]
    public int GetUserId()
    {
        return _userId;
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpGet("getUserById")]
    public GetUserViewModel GetUserById(int id)
    {
        return _userService.GetUserById(id);
    }
    [HttpPost("addProgress")]
    public ProgressViewModel AddProgress(ProgressViewModel progress)
    {
        return _userService.AddProgress(progress);
    }
}