using Application.DTOs.Base;
using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
    public class BoardColumnsDTO : BaseDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }

        public int BoardId { get; set; }

    }
}
