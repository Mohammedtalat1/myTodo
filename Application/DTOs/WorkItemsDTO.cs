using Application.DTOs.Base;
using Domain.Common.enums;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Xml.Linq;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
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
