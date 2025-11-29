using Application.DTOs.Base;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
    public class ProjectsDTO : BaseDTO
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
