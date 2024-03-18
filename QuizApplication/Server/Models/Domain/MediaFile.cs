using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models.Domain
{
    public class MediaFile
    {
        [Key]
        public Guid MediaFileId { get; set; }

        [Required]
        public Guid FkMediaTypeId { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        [NotNull]
        public string? MediaFileName { get; set; }

        [NotNull]
        public string? FileExtension { get; set; }

        [Required]
        public long FileSizeInBytes { get; set; }

        [NotNull]
        public string? FilePath { get; set; }


        // Navigation properties
        public virtual MediaType? MediaTypes { get; set; }
        public List<Question>? Questions { get; set; }
    }
}
