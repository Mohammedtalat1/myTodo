using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for a column within a Kanban board.</summary>
    public class BoardColumnsDTO : BaseDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }

        [Required]
        public int BoardId { get; set; }
    }
}
