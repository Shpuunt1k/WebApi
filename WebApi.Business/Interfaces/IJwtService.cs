using WebApi.Entity.Models.Jwt;

namespace WebApi.Business.Interfaces;

public interface IJwtService
{
    public Response? Token(string username, string password);
}
