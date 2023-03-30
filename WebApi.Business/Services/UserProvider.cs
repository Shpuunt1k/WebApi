using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.Interfaces;

namespace WebApi.Business.Services;
public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _context;

    public UserProvider(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public int GetUserId()
    {
        try
        {
            return int.Parse(_context.HttpContext.User.Claims.First(i => i.Type == "UserId").Value);
        }
        catch
        {
            return 0;
        }
    }
}