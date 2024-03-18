using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models.Domain
{
    public class QuizItem
    {
        [Key]
        public Guid QuizItemId { get; set; }

        [Required]
        public string? FkUserId { get; set; }

        [Required]
        public Guid FkQuestionId { get; set; }

        [NotNull]
        public bool IsScored { get; set; }

        [NotNull]
        public int TimeSpent { get; set; }

        [NotNull]
        public DateTime Started_At { get; set; }

        // Navigation properties
        public virtual ApplicationUser? ApplicationUsers { get; set; }
        public virtual Question? Questions { get; set; }

    }
}
