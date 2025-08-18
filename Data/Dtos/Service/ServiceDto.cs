using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Data.Dtos.Service;

public class ServiceDto {
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [Required] public required string Title { get; set; }
    [Required] public required string Content { get; set; }
}
