using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace QuizApplication.Shared.DTO
{
    public class QuestionRequestDto
    {
        [NotNull]
        public string? MediaFileName { get; set; }

        [NotNull]
        //[JsonPropertyName("title")]
        public string? Title { get; set; }

        [NotNull]
        [Column(TypeName = "varchar(25)")]
        [StringLength(25, ErrorMessage = "Path length can't be more than 25.")]
        public string? QuestionPath { get; set; }

        [NotNull]
        [Range(1, 45, ErrorMessage = "The time limit must be between 1 and 45min.")]
        public int TimeLimit { get; set; } = 5;

        [NotNull]
        //[JsonPropertyName("isPublished")]
        public bool IsPublished { get; set; } = false;
    }
}
