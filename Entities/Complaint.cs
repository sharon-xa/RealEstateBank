using System.ComponentModel.DataAnnotations;

using RealEstateBank.Data.Enums;

namespace RealEstateBank.Entities;

public class Complaint : BaseEntity<int> {
    [Required] public required string FullName { get; set; }
    [Required] public required string PhoneNumber { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required Priority Priority { get; set; }
}
