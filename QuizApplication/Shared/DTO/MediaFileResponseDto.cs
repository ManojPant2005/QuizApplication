using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Shared.DTO
{
    public class MediaFileResponseDto
    {
        [NotNull]
        public string? MediaFileName { get; set; }
        public string? StoredFileName { get; set; }
    }
}
