using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Theme
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public User Author { get; set; }
    public List<Message> Messages { get; set; }
    public Section Section { get; set; }
    public int SectionId { get; set; }
    public int AuthorId { get; set; }

    public Theme()
    {
        Messages = new List<Message>();
        CreatedDate = DateTime.Now;
    }
}
