using Microsoft.AspNetCore.Identity;
using QuizApplication.Server.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [NotNull]
        [Display(Name = "Nickname")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "String length must be greater than or equal 4 characters and not exceed 15 characters")]
        public string? Nickname { get; set; }


        // Navigation properties
        public List<Question>? Questions { get; set; }
        public List<QuizItem>? QuizItems { get; set; }
    }
}
