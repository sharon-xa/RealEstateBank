using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Entities;

public class BaseEntity<TId> {
    [Key]
    public TId Id { get; set; } = default!;
    public bool Deleted { get; set; } = false;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
