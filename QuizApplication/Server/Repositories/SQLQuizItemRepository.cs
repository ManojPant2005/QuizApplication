
using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Data;
using QuizApplication.Server.Models.Domain;
using QuizApplication.Server.Repositories;
using QuizApplication.Shared.DTO;

namespace BlazorQuizWASM.Server.Repositories
{
    public class SQLQuizItemRepository : IQuizItemRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLQuizItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuizItem> CreateAsync(QuizItem quizItem)
        {
            if (_context.QuizItems == null)
            {
                throw new Exception("Entity 'QuizItems' not found.");
            }

            await _context.QuizItems.AddAsync(quizItem);
            await _context.SaveChangesAsync();
            return quizItem;
        }

        public async Task<List<QuizItem>> GetScore(string fkUserId)
        {
            if (_context.QuizItems == null)
            {
                throw new Exception("Entity 'QuizItems' not found.");
            }

            var existingQuizItem = await _context.QuizItems
                .Where(x => x.FkUserId == fkUserId)
                .ToListAsync();

            return existingQuizItem;
        }

     
    }
}
