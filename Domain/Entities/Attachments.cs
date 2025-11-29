using Domain.Entities.Base;
using TODO.Domain.Entities;

public class Attachments : BaseEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public long FileSize { get; set; }
    public string? Description { get; set; }

    public int WorkItemId { get; set; }
    public WorkItems WorkItem { get; set; }
}
