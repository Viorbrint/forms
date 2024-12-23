using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

// TODO: add Async to all method names

public class TemplateService(TemplateRepository templateRepository)
{
    public async Task<List<Template>> GetByUserAsync(string userId)
    {
        var spec = new Specification<Template>(
            t => t.AuthorId == userId,
            q => q.OrderByDescending(t => t.CreatedAt)
        );
        // TODO: refactor
        spec.AddInclude(t => t.Topic);
        var result = await templateRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task<Template?> GetByIdAsync(string templateId)
    {
        var result = await templateRepository.GetByIdAsync(templateId);
        return result;
    }

    public async Task AddAsync(Template template)
    {
        await templateRepository.AddAsync(template);
    }

    public async Task IsReady(string id)
    {
        // TODO: check all req fields for emptiness
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task PublishTemplateAsync(string id)
    {
        await IsReady(id);
        throw new NotImplementedException();
    }

    public async Task HideTemplateAsync(string id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
