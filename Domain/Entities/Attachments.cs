using System.ComponentModel.DataAnnotations;
using Domain.Entities.Base;

namespace TODO.Domain.Entities
{
    /// <summary>
    /// Represents a file attachment linked to a work item.
    /// </summary>
    public class Attachments : BaseEntity
    {
        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// URL or relative path to the stored file.
        /// </summary>
        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FileType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public string? Description { get; set; }

        public int WorkItemId { get; set; }
        public WorkItems? WorkItem { get; set; }
    }
}
