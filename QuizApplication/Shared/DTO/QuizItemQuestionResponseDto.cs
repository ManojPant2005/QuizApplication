using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuizApplication.Shared.DTO
{
    public class QuizItemQuestionResponseDto
   {
        public QuizItemResponseDto? QuizItem { get; set; }
        public string? QuestionPath { get; set; }
        public string? QuestionTitle { get; set;}
    }
}
