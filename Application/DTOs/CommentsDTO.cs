using Application.DTOs.Base;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
    public class CommentsDTO : BaseDTO
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        public int TaskId { get; set; }

        public int UserId { get; set; }
    }
}
