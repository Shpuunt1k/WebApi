using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class ThemeViewModel
{
    public List<GetThemeViewModel> Themes { get; set; }
    public int TotalCount { get; set; }
    public int SectionId { get; set; }
    public string SectionTitle { get; set; }
    public string SectionDescription { get; set; }
    public int? SectionCategoryId { get; set; }
    public string SectionCategoryName { get; set; }
}
public class ThemeCountViewModel : ThemeViewModel
{
    //public int TotalCount { get; set; }
}
