using Application.DTOs.Base;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for a Kanban board belonging to a project.</summary>
    public class BoardsDTO : BaseDTO
    {
        public int ProjectId { get; set; }
    }
}
