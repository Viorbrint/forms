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

    public async Task UpdateAsync(User user)
    {
        await userRepository.UpdateAsync(user);
    }

    public async Task<bool> IsSyncedWithSalesforceById(string userId)
    {
        var spec = new SpecificationSingle<User>(u => u.Id == userId);
        var user =
            await userRepository.GetBySpecificationSingleAsync(spec)
            ?? throw new ArgumentException("User not found");
        return user.IsSyncWithSalesforce;
    }

    public async Task SyncWithSalesforce(string? userId)
    {
        var spec = new SpecificationSingle<User>(u => u.Id == userId);
        var user =
            await userRepository.GetBySpecificationSingleAsync(spec)
            ?? throw new ArgumentException("User not found");
        user.IsSyncWithSalesforce = true;
        await UpdateAsync(user);
    }
}
