using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.Models;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Interfaces;

public interface IMessageService
{
    public MessageViewModel GetMessages(int themeId, int skip, int take);
    public WriteMessageViewModel WriteMessage(WriteMessageViewModel NewMessage);
    public UpdateMessageViewModel UpdateMessage(UpdateMessageViewModel messages);
    public bool DeleteMessage(List<int> ids);
}
