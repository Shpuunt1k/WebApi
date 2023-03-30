using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity.Models;
using WebApi.Entity.Models.Jwt;

namespace WebApi.Business.Services;

public class JwtService : IJwtService
{
    private readonly IRepository<User> _userRepository;
    public JwtService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public Response? Token(string username, string password)
    {
        var identity = GetIdentity(username, password);

        if (identity == null)
        {
            return null;
        }
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var person = _userRepository.GetAll().FirstOrDefault(x => x.Login == identity.Name);

        return new Response
        {
            AccessToken = encodedJwt,
            UserName = identity.Name,
            UserRole = person.Role.ToString()
        };
    }
    private ClaimsIdentity? GetIdentity(string username, string password)
    {
        var person = _userRepository.GetAll().FirstOrDefault(x => x.Login == username && x.Password == password);

        if (person == null) return null;

        var claims = new List<Claim>
        {
            new Claim("UserId", person.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString()),
        };
        return new ClaimsIdentity(claims, "Token");
    }
}
