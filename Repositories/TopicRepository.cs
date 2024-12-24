using Forms.Data;
using Forms.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forms.Repositories;

public class TopicRepository
{
    private ApplicationDbContext Context { get; }

    private DbSet<Topic> Topics { get; }

    public TopicRepository(ApplicationDbContext context)
    {
        Context = context;
        Topics = context.Set<Topic>();
    }

    public async Task<List<Topic>> GetAllAsync()
    {
        return await Topics.ToListAsync();
    }

    public async Task<Topic?> GetByNameAsync(string name)
    {
        return await Topics.FirstOrDefaultAsync(t => t.TopicName == name);
    }
}
