using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity.Viewmodels;


namespace WebApi.Business.Services;
public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private IUserProvider _userProvider { get; set; }
    private readonly int _userId;
    public UserService(IRepository<User> userRepository, IUserProvider userProvider)
    {
        _userRepository = userRepository;
        _userProvider = userProvider;
        _userId = _userProvider.GetUserId();
    }

    public UserViewModel GetUsers(int skip, int take)
    {
        var users = _userRepository.Include(
            user => user.Likes).Skip(skip).Take(take);

        var result = new UserViewModel
        {
            Users = new List<GetUserViewModel>(),
        };

        result.Users = users.Select(user => new GetUserViewModel
        {
            Id = user.Id,
            Login = user.Login,
            Password = user.Password,
            Role = user.Role,
        }).ToList();

        return result;
    }
    public GetUserViewModel GetUserById(int id)
    {
        var user = _userRepository.GetById(id);
        var users = _userRepository.Include(
        user => user.Likes);
        var result = new GetUserViewModel
        {
            Id = id,
            Login = user.Login,
            Password = user.Password,
            Role = user.Role,
        };
        return result;

    }
    public UserItemViewModel Create(UserItemViewModel CreatedUser)
    {
        var user = new User
        {
            Login = CreatedUser.Login,
            Password = CreatedUser.Password,
        };
        var getuser = _userRepository.GetAll().ToList()
            .Any(usr => usr.Login == user.Login);
        if (getuser == false)
        {
            var created = _userRepository.Create(user);
            return new UserItemViewModel
            {
                Id = created.Id,
                Login = created.Login,
                Password = created.Password,
            };
        }
        else
            return null;
    }
    public UpdateUserViewModel UpdateRange(UpdateUserViewModel users)
    {
        var existingUsers = _userRepository.GetAll().ToList()
            .Where(usr => users.Users.Any(user => user.Id == usr.Id)).ToList();

        var updated = (from user in users.Users
                       where existingUsers.Any(usr => usr.Id == user.Id)
                       select new User
                       {
                           Id = user.Id,
                           Login = user.Login,
                           Password = user.Password,
                       }).ToList();
        _userRepository.UpdateRange(updated);
        var updatedUsers = _userRepository.GetAll().ToList()
            .Where(usr => users.Users.Any(user => user.Id == usr.Id)).ToList();
        return new UpdateUserViewModel
        {
            Users = updatedUsers.Select(user => new CreateUserViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
            }).ToList()
        };
    }

    public bool DeleteRange(List<int> ids)
    {
        var existingUsers = _userRepository.GetAll().ToList()
    .Where(usr => ids.Any(id => id == usr.Id)).ToList();

        return _userRepository.DeleteRange(existingUsers);
    }
}
