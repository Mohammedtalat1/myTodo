using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for roles (system-level).</summary>
    public class RolesDTO : BaseDTO
    {
        [Required, MaxLength(100)]
        public string Name_Ar { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Name_En { get; set; }
    }
}
