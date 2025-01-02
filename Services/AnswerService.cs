using Forms.Data.Entities;
using Forms.Repositories;

namespace Forms.Services;

public class AnswerService(IRepository<Answer> answerRepository)
{
    public async Task<IEnumerable<Answer>> GetByFormAsync(string formId)
    {
        var spec = new Specification<Answer>(
            a => a.FormId == formId,
            q => q.OrderBy(t => t.Question.Order)
        );
        spec.AddInclude(t => t.Question);
        var result = await answerRepository.GetBySpecificationAsync(spec);
        return result;
    }

    public async Task UpdateRangeAsync(IEnumerable<Answer> answers)
    {
        await answerRepository.UpdateRangeAsync(answers);
    }
}
