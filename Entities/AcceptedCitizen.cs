using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Entities;

public class AcceptedCitizen : BaseEntity<int> {
    [Required]
    public required string FullName { get; set; }
    public bool AcceptanceState { get; set; }

    // Foreign key
    public int BranchId { get; set; }
    // Navigation property
    public Branch Branch { get; set; } = null!;
}
