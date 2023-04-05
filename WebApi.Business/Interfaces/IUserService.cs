using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface IUserService
{
    public UserViewModel GetUsers(int skip, int take);
    public GetUserViewModel GetUserById(int id);
    public UserItemViewModel Create(UserItemViewModel CreatedUser);
    public UpdateUserViewModel UpdateRange(UpdateUserViewModel users);
    public bool DeleteRange(List<int> ids);
}