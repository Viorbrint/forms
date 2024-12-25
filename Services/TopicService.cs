using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class TopicService(IRepository<Topic> topicRepository)
{
    public async Task<IEnumerable<Topic>> GetAllAsync()
    {
        var spec = new Specification<Topic>();
        var result = await topicRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task<IEnumerable<string>> GetAllNamesAsync()
    {
        var topics = await GetAllAsync();
        return topics.Select(t => t.TopicName).ToList();
    }

    public async Task<Topic?> GetByNameAsync(string name)
    {
        var result = await topicRepository.GetOneByCriteriaAsync((t) => t.TopicName == name);
        return result;
    }
}
