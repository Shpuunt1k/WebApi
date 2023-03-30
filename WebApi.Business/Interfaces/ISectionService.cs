using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface ISectionService
{
    public SectionCountViewModel GetSections(int skip, int take);
    public CreateSectionViewModel CreateSection(CreateSectionViewModel CreatedSection);
    public UpdateSectionViewModel UpdateSection(UpdateSectionViewModel sections);
    public bool DeleteSection(List<int> ids);
}
