using QuizApplication.Server.Models.Domain;
using QuizApplication.Shared.DTO;

namespace QuizApplication.Server.Repositories
{
    public interface IQuizItemRepository
    {
        Task<QuizItem> CreateAsync(QuizItem quizItem);
        Task<List<QuizItem>> GetScore(string fkUserId);
    }
}
