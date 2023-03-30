using WebApi.Entity.Models;
using WebApi.Business.Interfaces;
using WebApi.DataAccess.Interfaces;
using WebApi.Entity;
using WebApi.Entity.Viewmodels;

namespace WebApi.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public CategoryCountViewModel GetCategories(int skip, int take)
    {
        var categories = _categoryRepository.Include(
            category => category.Sections
            ).Skip(skip).Take(take);

        var result = new CategoryCountViewModel
        {
            Categories = new List<NewCategoryViewModel>(),
            TotalCount = _categoryRepository.GetAll().Count()
        };

        result.Categories = categories.Select(category => new NewCategoryViewModel
        {
            Id = category.Id,
            Title = category.Title,
            Sections = category.Sections.Select(section => new NewSectionViewModel
            {
                Id = section.Id,
                Title = section.Title,
                Description = section.Description,
                CountThemes = section.Themes.Count(),
                CategoryId = category.Id
            }).ToList()
        }).ToList();

        return result;
    }
    public CreateCategoryViewModel CreateCategory(CreateCategoryViewModel CreatedCategory)
    {
        var category = new Category
        {
            Title = CreatedCategory.Title
        };
        var created = _categoryRepository.Create(category);
        return new CreateCategoryViewModel
        {
            Id = (int)created.Id,
            Title = created.Title
        };
    }
    public UpdateCategoryViewModel UpdateRange(UpdateCategoryViewModel categories)
    {
        var existingCategories = _categoryRepository.GetAll().ToList()
            .Where(ctg => categories.Categories.Any(category => category.Id == ctg.Id)).ToList();

        var updated = (from category in categories.Categories
                       where existingCategories.Any(ctg => ctg.Id == category.Id)
                       select new Category
                       {
                           Id = category.Id,
                           Title = category.Title,
                           //Sections = category.Sections.Select(section => new Section
                           //{
                           //    Id = section.Id,
                           //}).ToList()
                       }).ToList();

        _categoryRepository.UpdateRange(updated);

        var updatedCategories = _categoryRepository.GetAll().ToList()
            .Where(ctg => categories.Categories.Any(category => category.Id == ctg.Id)).ToList();
        return new UpdateCategoryViewModel
        {
            Categories = updatedCategories.Select(category => new CreateCategoryViewModel
            {
                Id = (int)category.Id,
                Title = category.Title,
                //Sections = category.Sections.Select(section => new NewSectionViewModel
                //{
                //    Id = section.Id,
                //}).ToList()
            }).ToList()
        };
    }
    public bool DeleteRange(List<int> ids)
    {
        var existingCategories = _categoryRepository.GetAll().ToList()
    .Where(ctg => ids.Any(id => id == ctg.Id)).ToList();

        return _categoryRepository.DeleteRange(existingCategories);
    }
}
