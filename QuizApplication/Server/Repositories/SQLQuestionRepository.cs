using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Data;
using QuizApplication.Server.Models.Domain;
using System.IO;

namespace QuizApplication.Server.Repositories
{
    public class SQLQuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLQuestionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Question?> CreateAsync(Question question)
        {
            if (_context.Questions == null)
            {
                throw new Exception("Entity 'Questions' not found.");
            }

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Question?> DeleteAsync(Guid id)
        {
            if (_context.Questions == null)
            {
                throw new Exception("Entity 'Questions' not found.");
            }

            var existingQuestion = await _context.Questions.FirstOrDefaultAsync(x => x.QuestionId == id);

            if (existingQuestion == null)
            {
                return null;
            }

            _context.Questions.Remove(existingQuestion);
            await _context.SaveChangesAsync();
            return existingQuestion;
        }

        public async Task<List<Question>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var questions = _context?.Questions?.AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    questions = questions?.Where(x => x.Title.Contains(filterQuery));
                }
            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    questions = isAscending ? questions?.OrderBy(x => x.Title) : questions?.OrderByDescending(x => x.Title);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            if (questions == null)
            {
                throw new Exception("No questions found.");
            }

            return await questions
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();
        }
    

            public async Task<Question> GetQuestionByPath(string questionPath)
        {
            if (_context.Questions == null)
            {
                throw new Exception("Entity 'Questions' not found.");
            }

            var question = await _context.Questions
                .Where(q => q.QuestionPath == questionPath)
                .FirstOrDefaultAsync();

            return question ?? throw new Exception("Question not found.");
        }

        public async Task<Question> GetQuestionByPathAndUserAsync(string? path, string? fkUserId)
        {
            if (_context.Questions == null)
            {
                throw new Exception("Entity 'Questions' not found.");
            }

            path = path?.ToLower().Replace(" ", "-");
            var question = await _context.Questions
                .Where(q => q.QuestionPath == path && q.FkUserId == fkUserId)
                .FirstOrDefaultAsync();

            return question ?? throw new Exception("Question not found.");
        }

        public async Task<Question?> UpdateAsync(Guid id, Question question)
        {
            if (_context.Questions == null)
            {
                throw new Exception("Entity 'Questions' not found.");
            }

            var existingQuestion = await _context.Questions.FirstOrDefaultAsync(x => x.QuestionId == id);

            if (existingQuestion == null)
            {
                return null;
            }

            existingQuestion.Title = question.Title;
            existingQuestion.QuestionPath = question.QuestionPath;
            existingQuestion.TimeLimit = question.TimeLimit;
            existingQuestion.IsPublished = question.IsPublished;
            existingQuestion.FkUserId = question.FkUserId;
            existingQuestion.FkFileId = question.FkFileId;

            _context.Entry(existingQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingQuestion;
        }
    }
    
}
