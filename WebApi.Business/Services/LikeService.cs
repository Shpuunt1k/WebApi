using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.Interfaces;
using WebApi.Entity.Models;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class LikeService : ILikeService
{
    private readonly IRepository<Like> _likeRepository;
    private IUserProvider _userProvider { get; set; }
    private readonly int _userId;
    public LikeService(IRepository<Like> likeRepository, IUserProvider userProvider)
    {
        _userProvider = userProvider;
        _userId = _userProvider.GetUserId();
        _likeRepository = likeRepository;
    }
    public LikeViewModel GetLikes(int skip, int take)
    {
        var likes = _likeRepository.Include(
            like => like.Author).Where(like => like.AuthorId == _userId).Skip(skip).Take(take);
        var result = new LikeViewModel
        {
            Likes = new List<GetLikeViewModel>(),
        };
        result.Likes = likes.Select(like => new GetLikeViewModel
        {
            Id = like.Id,
            Type = like.Type,
            Film = like.Film,
            AuthorId = like.Author.Id,
        }).ToList();
        return result;
    }
    public SetLikeViewModel SetLike(SetLikeViewModel NewLike)
    {
        var like = new Like
        {
            Type = NewLike.Type,
            Film = NewLike.Film,
            AuthorId = _userId,
        };
        var created = _likeRepository.Create(like);
        return new SetLikeViewModel
        {
            Id = created.Id,
            Type = created.Type,
            Film = created.Film,
            AuthorId = created.AuthorId,
        };
    }
    
    public bool DeleteLike(List<int> ids)
    {
        var existingLikes = _likeRepository.GetAll().ToList()
    .Where(m => ids.Any(id => id == m.Id && _userId == m.AuthorId)).ToList();

        return _likeRepository.DeleteRange(existingLikes);
    }
}
