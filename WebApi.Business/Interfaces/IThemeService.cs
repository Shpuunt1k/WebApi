using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;


namespace WebApi.Business.Interfaces;

public interface IThemeService
{
    public ThemeViewModel GetThemes(int sectionId, int skip, int take);
    public CreateThemeViewModel CreateTheme(CreateThemeViewModel CreatedTheme);
    public UpdateThemeViewModel UpdateTheme(UpdateThemeViewModel themes);
    public bool DeleteTheme(List<int> ids);
}
