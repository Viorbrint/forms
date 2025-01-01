using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class UserService(IRepository<User> userRepository)
{
    public async Task<IEnumerable<User>> SearchUsers(string searchQuery)
    {
        var spec = new Specification<User>(u =>
            u.UserName!.Contains(searchQuery) || u.Email!.Contains(searchQuery)
        );
        var users = await userRepository.GetBySpecificationAsync(spec);
        return users;
    }

    public async Task<List<User>> GetWithAccessToTemplate(string templateId)
    {
        var spec = new Specification<User>(
            u => u.TemplateAccesses.Any(ta => ta.Template.Id == templateId),
            q => q.OrderDescending()
        );
        spec.AddInclude(u => u.TemplateAccesses);
        var users = await userRepository.GetBySpecificationAsync(spec);
        return users.ToList();
    }
}
