using Application.DTOs.Base;
using Domain.Entities.Base;
using TODO.Domain.Entities;

namespace TODO.Application.Entities
{
    public class AttachmentsDTO : BaseDTO
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string? Description { get; set; }

        public int WorkItemId { get; set; }
    }
}
