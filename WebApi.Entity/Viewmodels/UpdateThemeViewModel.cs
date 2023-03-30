using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Viewmodels;

public class UpdateThemeViewModel
{
    public List<CreateThemeViewModel> Themes { get; set; }
}
