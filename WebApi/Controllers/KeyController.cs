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

[EnableCors]
public class KeyController : Controller
{
    private readonly IKeyService _keyService;
    public KeyController(IKeyService keyService)
    {
        _keyService = keyService;
    }
    [HttpGet("getKey")]
    public Key GetKey()
    {
        return _keyService.GetKey();
    }
}
