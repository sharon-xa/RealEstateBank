using System.ComponentModel.DataAnnotations;

using RealEstateBank.Data.Enums;

namespace RealEstateBank.Entities;

public class Video : BaseEntity<int> {
    [Required] public required string Title { get; set; }
    [Required] public required Priority Priority { get; set; }
    public string? Description { get; set; }
    public required string Url { get; set; }
}
