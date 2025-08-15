using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Entities;

public class Advertisement : BaseEntity<int> {
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
}
