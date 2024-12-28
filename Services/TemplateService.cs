using Forms.Data.Entities;
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
        var result = await templateRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task<Template?> GetByIdAsync(string templateId)
    {
        var spec = new SpecificationSingle<Template>(t => t.Id == templateId);
        spec.AddInclude(t => t.Topic);
        spec.AddInclude(t => t.Tags);
        spec.AddInclude(t => t.Questions.OrderBy(q => q.Order));
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

    private bool IsReady(Template template)
    {
        // TODO: check all req fields for emptiness
        // TODO: implement
        return true;
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
        if (!IsReady(template))
        {
            return;
        }
        template.IsPublished = true;
    }

    private void HideTemplate(Template template)
    {
        if (!IsReady(template))
        {
            return;
        }
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
}
