using Application.DTOs.Base;
using Domain.Common.enums;
using Domain.Entities.Base;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
    public class ProjectMemberDTO : BaseDTO
    {
        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public RoleInProjectEnum RoleInProject { get; set; } = RoleInProjectEnum.Developer;
    }
}
