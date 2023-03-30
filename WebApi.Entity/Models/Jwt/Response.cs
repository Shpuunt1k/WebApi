namespace WebApi.Entity.Models.Jwt;

public class Response
{
    public string AccessToken { get; set; }
    public string? UserName { get; set; }
    public string UserRole { get; set; }

    public Response() { }
}
