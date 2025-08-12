using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Entities;

public class BaseEntity<TId> {
    [Key]
    public TId Id { get; set; } = default!;

    public bool Deleted { get; set; } = false;
    public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
}
