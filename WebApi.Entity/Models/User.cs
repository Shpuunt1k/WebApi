using WebApi.Entity.Models.Enum;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApi.Entity.Models;
public class User
{
    [Key]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    [EnumDataType(typeof(UserRole))]
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole Role { get; set; }
    public List<Like> Likes { get; set; }
    public User()
    {
        Likes = new List<Like>();
    }
}
