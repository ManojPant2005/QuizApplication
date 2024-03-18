using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Server.CustomActionFilters;
using QuizApplication.Server.Models.Domain;
using QuizApplication.Server.Repositories;
using QuizApplication.Shared.DTO;
using System.Security.Claims;

namespace QuizApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswerController(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            this._answerRepository = answerRepository;
            this._questionRepository = questionRepository;
        }

        // CREATE Answers
        // POST: api/Answers/upload
        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<IEnumerable<AnswerRequestDto>>> PostAnswer([FromBody] AnswersQuestionRequestDto answerRequestDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // get question id
            var question = await _questionRepository.GetQuestionByPathAndUserAsync(answerRequestDto.Path, userId);
            var questionId = question.QuestionId;

            var answers = new List<Answer>();
            {
                if (answerRequestDto.Answers == null)
                {
                    return Problem("No answers found", statusCode: 500);
                }

                foreach (var answer in answerRequestDto.Answers)
                {
                    answers.Add(new Answer
                    {
                        Content = answer.Content,
                        IsCorrect = answer.IsCorrect,
                        FkQuestionId = questionId
                    });
                }
            };

            await _answerRepository.CreateAsync(answers);

            answerRequestDto.Answers.Select(a => new AnswerRequestDto
            {
                Content = a.Content,
                IsCorrect = a.IsCorrect
            });

            return Ok(answerRequestDto);
        }
    }
}
