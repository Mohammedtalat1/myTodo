using Application.DTOs.Base;
using Domain.Common.enums;
using System.ComponentModel.DataAnnotations;

namespace TODO.Application.DTOs
{
    /// <summary>DTO for a work item (Task, Bug, Epic, etc.) within a project.</summary>
    public class WorkItemsDTO : BaseDTO
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public WorkItemTypesEnum Type { get; set; }

        [Required]
        public WorkItemStatusEnum Status { get; set; } = WorkItemStatusEnum.Todo;

        [Required]
        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;

        [Required]
        public int ProjectId { get; set; }

        public int? AssignedUserId { get; set; }
        public int? ParentId { get; set; }
        public int? Effort { get; set; }
        public int? ColumnId { get; set; }
    }
}
