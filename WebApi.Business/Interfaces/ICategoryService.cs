using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface ICategoryService
{
    public CategoryCountViewModel GetCategories(int skip, int take);
    public CreateCategoryViewModel CreateCategory(CreateCategoryViewModel CreatedCategory);
    public UpdateCategoryViewModel UpdateRange(UpdateCategoryViewModel categories);
    public bool DeleteRange(List<int> ids);
}
