using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class ThemeService : IThemeService
{
    private readonly IRepository<Section> _sectionRepository;
    private readonly IRepository<Theme> _themeRepository;
    private readonly IRepository<Message> _messageRepository;
    private IUserProvider _userProvider { get; set; }
    private readonly int _userId;
    public ThemeService(IRepository<Theme> themeRepository, IUserProvider userProvider,
        IRepository<Message> messageRepository, IRepository<Section> sectionRepository)
    {
        _themeRepository = themeRepository;
        _userProvider = userProvider;
        _userId = _userProvider.GetUserId();
        _messageRepository = messageRepository;
        _sectionRepository = sectionRepository;
    }
    public ThemeViewModel GetThemes(int sectionId, int skip, int take)
    {
        var themes = _themeRepository.Include(
          theme => theme.Messages, theme => theme.Author, theme => theme.Section.Category)
            .Where(theme => theme.SectionId == sectionId)
            .Skip(skip).Take(take);
        var section = _sectionRepository.GetById(sectionId);
        var result = new ThemeViewModel
        {
            SectionId = sectionId,
            SectionTitle = section.Title,
            SectionDescription = section.Description,
            SectionCategoryId = section.CategoryId,
            SectionCategoryName = section.Category.Title,
            Themes = new List<GetThemeViewModel>(),
            TotalCount = themes.Count()
        };
        result.Themes = themes.Select(theme => new GetThemeViewModel
        {
            Id = theme.Id,
            Title = theme.Title,
            Description = theme.Description,
            CreatedDate = theme.CreatedDate,
            SectionId = theme.Section.Id,
            AuthorId = theme.Author.Id,
            AuthorName = theme.Author.Login,
            CountMessages = theme.Messages.Count()
        }).ToList();
        return result;
    }
    public CreateThemeViewModel CreateTheme(CreateThemeViewModel CreatedTheme)
    {

        var theme = new Theme
        {
            Title = CreatedTheme.Title,
            Description = CreatedTheme.Description,
            SectionId = CreatedTheme.SectionId,
            AuthorId = _userId,
        };
        var created = _themeRepository.Create(theme);
        return new CreateThemeViewModel
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            SectionId = created.SectionId,
            AuthorId = created.AuthorId,
            CreatedDate = created.CreatedDate,
        };
    }
    public UpdateThemeViewModel UpdateTheme(UpdateThemeViewModel themes)
    {
        var existingThemes = _themeRepository.GetAll().ToList()
            .Where(t => themes.Themes.Any(theme => theme.Id == t.Id && _userId == theme.AuthorId)).ToList();
        var updated = (from theme in themes.Themes
                       where existingThemes.Any(t => t.Id == theme.Id)
                       select new Theme
                       {
                           Id = theme.Id,
                           Title = theme.Title,
                           Description = theme.Description,
                           AuthorId = theme.AuthorId,
                           SectionId = theme.SectionId,
                           CreatedDate = theme.CreatedDate,
                       }).ToList();
        _themeRepository.UpdateRange(updated);
        var updatedThemes = _themeRepository.GetAll().ToList()
            .Where(t => themes.Themes.Any(theme => theme.Id == t.Id)).ToList();
        return new UpdateThemeViewModel
        {
            Themes = updatedThemes.Select(theme => new CreateThemeViewModel
            {
                Id = theme.Id,
                Title = theme.Title,
                Description = theme.Description,
                AuthorId = theme.AuthorId,
                SectionId = theme.SectionId,
                CreatedDate = theme.CreatedDate,
            }).ToList()
        };
    }
    public bool DeleteTheme(List<int> ids)
    {
        var existingThemes = _themeRepository.GetAll().ToList()
    .Where(t => ids.Any(id => id == t.Id && _userId == t.AuthorId)).ToList();

        return _themeRepository.DeleteRange(existingThemes);
    }
}
