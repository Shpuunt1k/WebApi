using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class SectionService : ISectionService
{
    private readonly IRepository<Section> _sectionRepository;
    public SectionService(IRepository<Section> sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }
    public SectionCountViewModel GetSections(int skip, int take)
    {
        var sections = _sectionRepository.Include(
            section => section.Themes, section => section.Category).Skip(skip).Take(take);

        var result = new SectionCountViewModel
        {
            Sections = new List<GetSectionViewModel>(),
            TotalCount = _sectionRepository.GetAll().Count()
        };
        result.Sections = sections.Select(section => new GetSectionViewModel
        {
            Id = section.Id,
            Title = section.Title,
            Description = section.Description,
            CategoryId = section.Category.Id,
            Themes = section.Themes.Select(theme => new GetThemeViewModel
            {
                Id = theme.Id,
            }).ToList()

        }).ToList();
        return result;
    }
    public CreateSectionViewModel CreateSection(CreateSectionViewModel CreatedSection)
    {
        var section = new Section
        {
            Title = CreatedSection.Title,
            Description = CreatedSection.Description,
            CategoryId = CreatedSection.CategoryId,
        };
        var created = _sectionRepository.Create(section);
        return new CreateSectionViewModel
        {
            Id = (int)created.Id,
            Title = created.Title,
            Description = created.Description,
            CategoryId = created.CategoryId,
        };
    }
    public UpdateSectionViewModel UpdateSection(UpdateSectionViewModel sections)
    {
        var existingSections = _sectionRepository.GetAll().ToList()
            .Where(s => sections.Sections.Any(section => section.Id == s.Id)).ToList();

        var updated = (from section in sections.Sections
                       where existingSections.Any(s => s.Id == section.Id)
                       select new Section
                       {
                           Id = section.Id,
                           Title = section.Title,
                           Description= section.Description,
                           CategoryId= section.CategoryId,
                       }).ToList();

        _sectionRepository.UpdateRange(updated);

        var updatedSections = _sectionRepository.GetAll().ToList()
            .Where(s => sections.Sections.Any(section => section.Id == s.Id)).ToList();
        return new UpdateSectionViewModel
        {
            Sections = updatedSections.Select(section => new CreateSectionViewModel
            {
                Id = section.Id,
                Title = section.Title,
                Description = section.Description,
                CategoryId = section.CategoryId,

            }).ToList()
        };
    }
    public bool DeleteSection(List<int> ids)
    {
        var existingSections = _sectionRepository.GetAll().ToList()
    .Where(s => ids.Any(id => id == s.Id)).ToList();

        return _sectionRepository.DeleteRange(existingSections);
    }
}
