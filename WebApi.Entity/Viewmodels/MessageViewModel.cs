using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class MessageViewModel
{
    public List<GetMessageViewModel> Messages { get; set; }
    public int TotalCount { get; set; }
    public int ThemeId { get; set; }
    public string ThemeName { get; set; }
    public string ThemeDescription { get; set; }
    public int ThemeSectionId { get; set; }
    public string ThemeSectionName { get; set; }
    public int ThemeAuthorId { get; set; }
    public string ThemeAuthorName { get; set; }
    //public GetThemeViewModel Theme { get; set; }
}
public class MessageCountViewModel : MessageViewModel
{
    //public int TotalCount { get; set; }
    //public int ThemeId { get; set; }
}
