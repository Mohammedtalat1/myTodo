using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for creating and updating projects.</summary>
    public class ProjectsDTO : BaseDTO
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
