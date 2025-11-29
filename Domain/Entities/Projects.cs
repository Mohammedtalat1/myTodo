using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Domain.Entities
{
    public class Projects : BaseEntity
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<ProjectMember>? Members { get; set; }
        public ICollection<Boards>? Boards { get; set; }
        public ICollection<WorkItems>? WorkItems { get; set; }

    }
}
