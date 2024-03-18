using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Shared.DTO
{
    public class QuizItemRequestDto
    {
        [NotNull]
        public bool IsScored { get; set; }

        [NotNull]
        public int TimeSpent { get; set; }

        [NotNull]
        public DateTime Started_At { get; set; }
    }
}
