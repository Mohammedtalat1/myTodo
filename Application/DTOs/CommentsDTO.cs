using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for comments on a work item.</summary>
    public class CommentsDTO : BaseDTO
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int TaskId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
