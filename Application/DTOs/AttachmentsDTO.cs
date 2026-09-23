using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for file attachments linked to a work item.</summary>
    public class AttachmentsDTO : BaseDTO
    {
        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FileType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public string? Description { get; set; }

        [Required]
        public int WorkItemId { get; set; }
    }
}
