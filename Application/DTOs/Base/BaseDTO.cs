namespace Application.DTOs.Base
{
    /// <summary>
    /// Base DTO aligned with BaseEntity. All domain DTOs inherit from this.
    /// </summary>
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
