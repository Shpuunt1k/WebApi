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
    public string Email { get; set; }
    [EnumDataType(typeof(UserRole))]
    [JsonConverter(typeof(StringEnumConverter))]
    public UserRole Role { get; set; }
    public List<Message> Messages { get; set; }
    public List<Theme> Themes { get; set; }
    public Progress? Progress;
    public int? ProgressId;
    public User()
    {
        Messages = new List<Message>();
        Themes = new List<Theme>();
    }
}
