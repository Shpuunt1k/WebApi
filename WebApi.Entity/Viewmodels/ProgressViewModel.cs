using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Viewmodels;

public class ProgressViewModel
{
    public int Id { get; set; }
    public int? Value { get; set; }
    public int UserId { get; set; }
}
