using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.DTO
{
    public class QuestionResponseDto
    {
        public string? MediaFileName { get; set; }
        public string? MediaFilePath { get; set; }
        public string? FileExtension { get; set; }
        public string? Title { get; set; }
        public string? QuestionPath { get; set; }
        public int TimeLimit { get; set; }
        public bool IsPublished { get; set; }
    }
}
