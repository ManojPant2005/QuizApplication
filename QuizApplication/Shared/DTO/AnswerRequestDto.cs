using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.DTO
{
    public class AnswerRequestDto
    {
        [NotNull]
        public string? Content { get; set; }

        [NotNull]
        public bool IsCorrect { get; set; }
    }
}
