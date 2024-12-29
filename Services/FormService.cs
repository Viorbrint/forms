using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class FormService(IRepository<Form> formRepository)
{
    public async Task<IEnumerable<Form>> GetByUserAsync(string userId)
    {
        var spec = new Specification<Form>(
            f => f.UserId == userId,
            q => q.OrderByDescending(t => t.CreatedAt)
        );
        spec.AddInclude(f => f.Template);
        var result = await formRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task DeleteByIdsAsync(IEnumerable<string> ids)
    {
        var spec = new Specification<Form>(t => ids.Contains(t.Id));
        await formRepository.DeleteBySpecificationAsync(spec);
    }
}
