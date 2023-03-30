using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface IKeyService
{
    public Key GetKey(); 
}
