using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class ThemeIdViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<Message> Messages { get; set; }
    public int SectionId { get; set; }
    public string SectionName { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int CountMessages { get; set; }
    public ThemeIdViewModel()
    {
        Messages = new List<Message>();
    }
}
