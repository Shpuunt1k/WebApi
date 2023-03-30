using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Models;

public class Key
{
    public int Id { get; set; }
    public string Value { get; set; }
    public bool Used { get; set; }
    public Key()
    {
        Used = false;
    }
}

