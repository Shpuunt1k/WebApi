using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public User Author { get; set; }
    public Theme Theme { get; set; }
    public int AuthorId { get; set; }
    public int ThemeId { get; set; }
    public Message()
    {
        CreatedDate = DateTime.Now;
    }
}
