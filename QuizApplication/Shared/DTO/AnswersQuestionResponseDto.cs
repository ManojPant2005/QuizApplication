using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizApplication.Shared.DTO
{
    public class AnswersQuestionResponseDto
    {
        public QuestionResponseDto? Question { get; set; }
        public List<AnswerRequestDto>? Answers { get; set; }
    }
}
