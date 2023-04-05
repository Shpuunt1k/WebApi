using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;

namespace WebApi.Entity.Viewmodels;

public class LikeViewModel
{
    public List<GetLikeViewModel> Likes { get; set; }
}

