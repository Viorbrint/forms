using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class TopicService(TopicRepository topicRepository)
{
    public async Task<List<Topic>> GetAllAsync()
    {
        var result = await topicRepository.GetAllAsync();
        return result;
    }

    public async Task<List<string>> GetAllNamesAsync()
    {
        var topics = await GetAllAsync();
        return topics.Select(t => t.TopicName).ToList();
    }

    public async Task<Topic?> GetByNameAsync(string name)
    {
        var result = await topicRepository.GetByNameAsync(name);
        return result;
    }
}
