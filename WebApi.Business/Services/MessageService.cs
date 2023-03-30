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

public class MessageService : IMessageService
{
    private readonly IRepository<Message> _messageRepository;
    private readonly IRepository<Theme> _themeRepository;
    private IUserProvider _userProvider { get; set; }
    private readonly int _userId;
    public MessageService(IRepository<Message> messageRepository, IUserProvider userProvider, IRepository<Theme> themeRepository)
    {
        _userProvider = userProvider;
        _userId = _userProvider.GetUserId();
        _messageRepository = messageRepository;
        _themeRepository = themeRepository;
    }
    public MessageViewModel GetMessages(int themeId, int skip, int take)
    {
        var messages = _messageRepository.Include(message => message.Theme,
            message => message.Author, message => message.Theme.Section)
            .Where(message => message.ThemeId == themeId)
            .Skip(skip).Take(take);
        var theme = _themeRepository.GetById(themeId);
        var result = new MessageViewModel
        {
            ThemeId = themeId,
            ThemeName = theme.Title,
            ThemeDescription = theme.Description,
            ThemeSectionId = theme.SectionId,
            ThemeSectionName = theme.Section.Title,
            ThemeAuthorId = theme.AuthorId,
            ThemeAuthorName = theme.Author.Login,
            Messages = new List<GetMessageViewModel>(),
            TotalCount = messages.Count()
        };
        result.Messages = messages.Select(message => new GetMessageViewModel
        {
            Id = message.Id,
            Text = message.Text,
            CreatedDate = message.CreatedDate,
            AuthorId = message.Author.Id,
            AuthorName = message.Author.Login,
            ThemeId = themeId,
        }).ToList();
        return result;
    }
    public WriteMessageViewModel WriteMessage(WriteMessageViewModel NewMessage)
    {
        var message = new Message
        {
            Text = NewMessage.Text,
            CreatedDate = NewMessage.CreatedDate,
            ThemeId = NewMessage.ThemeId,
            AuthorId = _userId,
        };
        var created = _messageRepository.Create(message);
        return new WriteMessageViewModel
        {
            Id = created.Id,
            Text = created.Text,
            CreatedDate = created.CreatedDate,
            ThemeId = created.ThemeId,
            AuthorId = created.AuthorId,
        };
    }
    public UpdateMessageViewModel UpdateMessage(UpdateMessageViewModel messages)
    {
        var existingMessages = _messageRepository.GetAll().ToList()
            .Where(m => messages.Messages.Any(message => message.Id == m.Id && _userId == message.AuthorId)).ToList();
        var updated = (from message in messages.Messages
                       where existingMessages.Any(m => m.Id == message.Id)
                       select new Message
                       {
                           Id = message.Id,
                           Text = message.Text,
                           AuthorId = message.AuthorId,
                           ThemeId = message.ThemeId,
                           CreatedDate = message.CreatedDate,
                       }).ToList();
        _messageRepository.UpdateRange(updated);
        var updatedMessages = _messageRepository.GetAll().ToList()
            .Where(m => messages.Messages.Any(message => message.Id == m.Id)).ToList();
        return new UpdateMessageViewModel
        {
            Messages = updatedMessages.Select(message => new WriteMessageViewModel
            {
                Id = message.Id,
                Text = message.Text,
                AuthorId = message.AuthorId,
                ThemeId = message.ThemeId,
                CreatedDate = message.CreatedDate,
            }).ToList()
        };

    }
    public bool DeleteMessage(List<int> ids)
    {
        var existingMessages = _messageRepository.GetAll().ToList()
    .Where(m => ids.Any(id => id == m.Id && _userId == m.AuthorId)).ToList();

        return _messageRepository.DeleteRange(existingMessages);
    }
}
