using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Entity.Models.Jwt;

public class AuthOptions
{
    public const string ISSUER = "AnthillRedditBotServer"; // издатель токена
    public const string AUDIENCE = "AnthillRedditBotClient"; // потребитель токена
    const string KEY = "Ti@Ya!=s0bak@1337";   // ключ для шифрации
    public const int LIFETIME = 1; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
