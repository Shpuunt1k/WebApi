using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface ILikeService
{
    public LikeViewModel GetLikes(int skip, int take);
    public SetLikeViewModel SetLike(SetLikeViewModel NewLike);
    public bool DeleteLike(List<int> ids);
}
