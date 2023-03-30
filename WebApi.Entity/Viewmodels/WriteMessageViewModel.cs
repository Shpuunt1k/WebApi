using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Viewmodels;

public class WriteMessageViewModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AuthorId { get; set; }
    public int ThemeId { get; set; }
    public WriteMessageViewModel()
    {
        CreatedDate = DateTime.Now;
    }
}
