using Domain.Common.enums;
using Domain.Entities.Base;
using TODO.Domain.Entities;

namespace TODO.Domain.Entities
{
    public class ProjectMember : BaseEntity
    {
        public int UserId { get; set; }
        public Users? User { get; set; }

        public int ProjectId { get; set; }
        public Projects? Project { get; set; }

        public RoleInProjectEnum RoleInProject { get; set; } = RoleInProjectEnum.Developer;
    }
}
