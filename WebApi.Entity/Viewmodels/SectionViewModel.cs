using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class SectionViewModel
{
    public List<GetSectionViewModel> Sections { get; set; }
}

public class SectionCountViewModel : SectionViewModel
{
    public int TotalCount { get; set; }
}