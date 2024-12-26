using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class TagService(IRepository<Tag> tagRepository)
{
    public async Task AddRangeAsync(IEnumerable<string> TagNames)
    {
        var spec = new Specification<Tag>(t => TagNames.Contains(t.TagName));
        var existingTags = await tagRepository.GetBySpecificationAsync(spec);
        var existingTagNames = existingTags.Select(t => t.TagName);
        var tags = TagNames.Except(existingTagNames).Select(name => new Tag { TagName = name });
        await tagRepository.AddRangeAsync(tags);
    }

    public async Task<IEnumerable<Tag>> GetTagsByNamesAsync(IEnumerable<string> names)
    {
        var spec = new Specification<Tag>(t => names.Contains(t.TagName));
        var result = await tagRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task<IEnumerable<string>> SearchTagNames(string searchQuery)
    {
        var spec = new Specification<Tag>(t => t.TagName.Contains(searchQuery));
        var tags = await tagRepository.GetBySpecificationAsync(spec);
        var result = tags.Select(t => t.TagName);
        return result;
    }
}
