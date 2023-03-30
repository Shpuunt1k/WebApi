using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Progress
{
    public int Id { get; set; }
    public int? Value { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}
