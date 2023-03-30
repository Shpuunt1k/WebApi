using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Viewmodels;

public class GetMessageViewModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int ThemeId { get; set; }
}
