using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Domain.Entities
{
    public class BoardColumns : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }

        public int BoardId { get; set; }
        public Boards? Board { get; set; }

        public ICollection<WorkItems>? Tasks { get; set; }
    }
}
