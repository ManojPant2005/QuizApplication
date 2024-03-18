using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models.Domain
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }

        [Required]
        public string? FkUserId { get; set; }

        [Required]
        public Guid FkFileId { get; set; }

        [NotNull]
        public string? Title { get; set; }

        [NotNull]
        public string? QuestionPath { get; set; }

        [NotNull]
        public int TimeLimit { get; set; }

        [NotNull]
        public bool IsPublished { get; set; }



        // Navigation properties
        public virtual ApplicationUser? ApplicationUsers { get; set; }
        public virtual MediaFile? MediaFiles { get; set; }
        public List<Answer>? Answers { get; set; }
        public List<QuizItem>? QuizItems { get; set; }
    }
}
