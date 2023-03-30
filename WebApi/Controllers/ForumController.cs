using Microsoft.AspNetCore.Mvc;
using WebApi.Entity.AppDbContext;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Business.Interfaces;
using WebApi.Entity.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Entity.Models;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[SwaggerTag("Форум")]
[EnableCors]
public class ForumController : Controller
{
    private readonly ISectionService _sectionService;
    private readonly ICategoryService _categoryService;
    private readonly IThemeService _themeService;
    private readonly IMessageService _messageService;
    public ForumController(ICategoryService categoryService,
        ISectionService sectionService, IThemeService themeService,
        IMessageService messageService)
    {
        _categoryService = categoryService;
        _sectionService = sectionService;
        _themeService = themeService;
        _messageService = messageService;
}
    [HttpGet("getCategories")]
    public CategoryCountViewModel GetCategories(int skip = 0, int take = 50)
    {
        return _categoryService.GetCategories(skip, take);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPost("createCategory")]
    public CreateCategoryViewModel CreateCategory(CreateCategoryViewModel category)
    {
        return _categoryService.CreateCategory(category);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPut("updateCategories")]
    public UpdateCategoryViewModel UpdateRange(UpdateCategoryViewModel categories)
    {
        return _categoryService.UpdateRange(categories);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpDelete("deleteCategory")]
    public bool DeleteRange(List<int> ids)
    {
        return _categoryService.DeleteRange(ids);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPost("createSection")]
    public CreateSectionViewModel CreateSection(CreateSectionViewModel section)
    {
        return _sectionService.CreateSection(section);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPut("updateSections")]
    public UpdateSectionViewModel UpdateSection(UpdateSectionViewModel sections)
    {
        return _sectionService.UpdateSection(sections);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpDelete("deleteSection")]
    public bool DeleteSection(List<int> ids)
    {
        return _sectionService.DeleteSection(ids);
    }
    [HttpGet("getThemes")]
    public ThemeViewModel GetThemes(int SectionId, int skip = 0, int take = 50)
    {
        return _themeService.GetThemes(SectionId, skip, take);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPost("createTheme")]
    public CreateThemeViewModel CreateTheme(CreateThemeViewModel theme)
    {
        return _themeService.CreateTheme(theme);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPut("updateThemes")]
    public UpdateThemeViewModel UpdateTheme(UpdateThemeViewModel themes)
    {
        return _themeService.UpdateTheme(themes);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpDelete("deleteTheme")]
    public bool DeleteTheme(List<int> ids)
    {
        return _themeService.DeleteTheme(ids);
    }
    [HttpGet("getMessages")]
    public MessageViewModel GetMessages(int themeId, int skip = 0, int take = 50)
    {
        return _messageService.GetMessages(themeId, skip, take);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPost("writeMessage")]
    public WriteMessageViewModel WriteMessage(WriteMessageViewModel message)
    {
        return _messageService.WriteMessage(message);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpPut("updateMessages")]
    public UpdateMessageViewModel UpdateMessage(UpdateMessageViewModel messages)
    {
        return _messageService.UpdateMessage(messages);
    }
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
    [HttpDelete("deleteMessage")]
    public bool DeleteMessage(List<int> ids)
    {
        return _messageService.DeleteMessage(ids);
    }
}
