using Domain.Entities.Base;

namespace TODO.Domain.Entities
{
    public class Boards : BaseEntity
    {
        public int ProjectId { get; set; }
        public Projects? Project { get; set; }

        public ICollection<BoardColumns>? Columns { get; set; }
    }
}
