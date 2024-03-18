using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models.Domain
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; }

        [Required]
        public Guid FkQuestionId { get; set; }

        [NotNull]
        public string? Content { get; set; }

        [NotNull]
        public bool IsCorrect { get; set; }



        // Navigation properties
        public virtual Question? Questions { get; set; }
    }
}
