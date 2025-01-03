using Forms.Data.Entities;
using Forms.Models.TemplateModels;
using Forms.Repositories;

namespace Forms.Services;

// TODO: add Async to all method names

public class TemplateService(IRepository<Template> templateRepository)
{
    public async Task<IEnumerable<Template>> GetByUserAsync(string userId)
    {
        var spec = new Specification<Template>(
            t => t.AuthorId == userId,
            q => q.OrderByDescending(t => t.CreatedAt)
        );
        // TODO: refactor
        spec.AddInclude(t => t.Topic);
        spec.AddInclude(t => t.Tags);
        spec.AddInclude(t => t.Questions.OrderBy(q => q.Order));
        spec.AddInclude(t => t.Likes);
        var result = await templateRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task<Template?> GetByIdAsync(string templateId)
    {
        var spec = new SpecificationSingle<Template>(t => t.Id == templateId);
        spec.AddInclude(t => t.Topic);
        spec.AddInclude(t => t.Tags);
        spec.AddInclude(t => t.Questions.OrderBy(q => q.Order));
        spec.AddInclude(t => t.Likes);
        spec.AddInclude(t => t.TemplateAccesses);
        var result = await templateRepository.GetBySpecificationSingleAsync(spec);
        return result;
    }

    public async Task AddAsync(Template template)
    {
        await templateRepository.AddAsync(template);
    }

    public async Task DeleteByIdAsync(string templateId)
    {
        var spec = new SpecificationSingle<Template>(t => t.Id == templateId);
        await templateRepository.DeleteBySpecificationSingleAsync(spec);
    }

    public async Task DeleteByIdsAsync(IEnumerable<string> ids)
    {
        var spec = new Specification<Template>(t => ids.Contains(t.Id));
        await templateRepository.DeleteBySpecificationAsync(spec);
    }

    public async Task UpdateAsync(Template template)
    {
        await templateRepository.UpdateAsync(template);
    }

    public async Task ToggleLike(string userId, Template template)
    {
        var UserLike = template.Likes.Find(l => l.UserId == userId);
        if (UserLike == null)
        {
            template.Likes.Add(new() { UserId = userId });
        }
        else
        {
            template.Likes.Remove(UserLike);
        }
        await UpdateAsync(template);
    }

    public async Task PublishByIdAsync(string id)
    {
        var spec = new SpecificationSingle<Template>(t => t.Id == id);
        var template = await templateRepository.GetBySpecificationSingleAsync(spec);
        if (template == null)
        {
            return;
        }
        PublishTemplate(template);
        await templateRepository.UpdateAsync(template);
    }

    public async Task HideByIdAsync(string id)
    {
        var spec = new SpecificationSingle<Template>(t => t.Id == id);
        var template = await templateRepository.GetBySpecificationSingleAsync(spec);
        if (template == null)
        {
            return;
        }
        HideTemplate(template);
        await templateRepository.UpdateAsync(template);
    }

    private void PublishTemplate(Template template)
    {
        template.IsPublished = true;
    }

    private void HideTemplate(Template template)
    {
        template.IsPublished = false;
    }

    public async Task PublishByIdsAsync(IEnumerable<string> ids)
    {
        var spec = new Specification<Template>(t => ids.Contains(t.Id));
        var templates = await templateRepository.GetBySpecificationAsync(spec);
        foreach (var t in templates)
        {
            t.IsPublished = true;
        }
        await templateRepository.UpdateRangeAsync(templates);
    }

    public async Task HideByIdsAsync(IEnumerable<string> ids)
    {
        var spec = new Specification<Template>(t => ids.Contains(t.Id));
        var templates = await templateRepository.GetBySpecificationAsync(spec);
        foreach (var t in templates)
        {
            t.IsPublished = false;
        }
        await templateRepository.UpdateRangeAsync(templates);
    }

    public async Task AddFormAsync(Form form, string templateId)
    {
        var template = await GetByIdAsync(templateId);
        if (template == null)
        {
            throw new NullReferenceException("Template not found");
        }
        template.Forms.Add(form);
        await templateRepository.UpdateAsync(template);
    }

    public async Task<IEnumerable<PresentTemplateModel>> GetLatestTemplatesAsync(int number)
    {
        var spec = new Specification<Template>(
            t => t.IsPublished == true,
            q => q.OrderByDescending(t => t.CreatedAt)
        );
        spec.ApplyPaging(0, number);
        spec.AddInclude(t => t.Forms);
        spec.AddInclude(t => t.Tags);
        spec.AddInclude(t => t.Likes);
        spec.AddInclude(t => t.Author);
        var templates = await templateRepository.GetBySpecificationAsync(spec);
        return templates.Select(t => new PresentTemplateModel()
        {
            Id = t.Id,
            Title = t.Title,
            ImageUrl = t.ImageUrl,
            AuthorName = t.Author.UserName!,
            Description = t.Description,
            FilledFormsCount = t.Forms.Count,
        });
    }

    public async Task<IEnumerable<PresentTemplateModel>> GetPopularTemplatesAsync(int number)
    {
        var spec = new Specification<Template>(
            t => t.IsPublished == true,
            q => q.OrderByDescending(t => t.Forms.Count())
        );
        spec.ApplyPaging(0, number);
        spec.AddInclude(t => t.Forms);
        spec.AddInclude(t => t.Tags);
        spec.AddInclude(t => t.Likes);
        spec.AddInclude(t => t.Author);
        var templates = await templateRepository.GetBySpecificationAsync(spec);
        return templates.Select(t => new PresentTemplateModel()
        {
            Id = t.Id,
            Title = t.Title,
            ImageUrl = t.ImageUrl,
            AuthorName = t.Author.UserName!,
            Description = t.Description,
            FilledFormsCount = t.Forms.Count,
        });
    }
}
