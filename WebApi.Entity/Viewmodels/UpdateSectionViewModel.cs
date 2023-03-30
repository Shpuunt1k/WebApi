using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class UpdateSectionViewModel
{
    public List<CreateSectionViewModel> Sections { get; set; }
}
