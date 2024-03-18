using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Repositories
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> CreateAsync(List<Answer> answers);
        Task<List<Answer>> GetAnswerToQuestionAsync(Guid fkQuestionId);
    }
}
