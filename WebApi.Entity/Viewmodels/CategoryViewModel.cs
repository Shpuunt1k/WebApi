using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class CategoryViewModel
{
    public List<NewCategoryViewModel> Categories { get; set; }
}

public class CategoryCountViewModel : CategoryViewModel
{
    public int TotalCount { get; set; }
}