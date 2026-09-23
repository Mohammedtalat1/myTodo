using Application.DTOs.Base;
using Domain.Common.enums;

namespace TODO.Application.DTOs
{
    /// <summary>DTO representing a user's membership in a project with a specific role.</summary>
    public class ProjectMemberDTO : BaseDTO
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public RoleInProjectEnum RoleInProject { get; set; } = RoleInProjectEnum.Developer;
    }
}
