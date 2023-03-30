using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.Viewmodels;

public class UpdateMessageViewModel
{
    public List<WriteMessageViewModel> Messages { get; set; }
}
