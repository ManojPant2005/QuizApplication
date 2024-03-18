using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Shared.DTO
{
    public class QuizItemResponseDto
    {
        public string? Nickname { get; set; }
        public bool IsScored { get; set; }
        public int TimeSpent { get; set; }
        public DateTime Started_At { get; set; }
    }
}
