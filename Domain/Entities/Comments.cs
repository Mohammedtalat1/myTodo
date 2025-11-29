using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Domain.Entities
{
    public class Comments : BaseEntity
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        public int TaskId { get; set; }
        public WorkItems? Task { get; set; }

        public int UserId { get; set; }
        public Users? User { get; set; }
    }
}
