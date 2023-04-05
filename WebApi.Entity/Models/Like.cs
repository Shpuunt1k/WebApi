using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Like
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Film { get; set; }
    public User Author { get; set; }
    public int AuthorId { get; set; }

}
