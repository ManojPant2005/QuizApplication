using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizApplication.Server.Models.Domain
{
    public class MediaType
    {
        [Key]
        public Guid MediaId { get; set; }

        [NotNull]
        [Column(TypeName = "nvarchar(15)")]
        public string? Mediatype { get; set; }


        // Navigation properties
        public List<MediaFile>? MediaFiles { get; set; }
    }
}
