using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class UserViewModel
{
    public List<GetUserViewModel> Users { get; set; }
    public int TotalCount { get; set; }
}

public class UserCountViewModel : UserViewModel
{
    
}